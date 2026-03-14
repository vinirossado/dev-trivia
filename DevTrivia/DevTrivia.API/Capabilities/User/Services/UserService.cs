using System.Text;
using DevTrivia.API.Capabilities.User.Database.Entities;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Jose;

namespace DevTrivia.API.Capabilities.User.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeProvider _timeProvider;

    public UserService(
        IUserRepository userRepository,
        ILogger<UserService> logger,
        IConfiguration configuration,
        TimeProvider timeProvider)
    {
        _userRepository = userRepository;
        _logger = logger;
        _configuration = configuration;
        _timeProvider = timeProvider;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        _logger.UserLoginAttempt(request.Email);

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        if (user is null || !VerifyPassword(request.Password, user.PasswordHash ?? string.Empty))
        {
            _logger.UserLoginFailed(request.Email);
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Update last login time
        user.LastLoginAt = _timeProvider.GetUtcNow().UtcDateTime;
        await _userRepository.UpdateAsync(user, cancellationToken);

        var token = GenerateJwtToken(user);
        _logger.UserLoginSuccessful(request.Email, user.Id);

        var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpirationInMinutes", 60);
        
        return new LoginResponse
        {
            Token = token,
            TokenType = "Bearer",
            UserId = user.Id,
            Email = user.Email,
            Name = user.Name,
            ExpiresAt = _timeProvider.GetUtcNow().UtcDateTime.AddMinutes(expirationMinutes)
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            _logger.UserRegistrationEmailExists(request.Email);
            throw new InvalidOperationException("Email already registered");
        }

        var user = new UserEntity
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            AuthProvider = "local",
            ExternalId = null,
            PreferredLanguage = request.PreferredLanguage
        };

        var createdUser = await _userRepository.AddAsync(user, cancellationToken);
        _logger.UserRegistered(createdUser.Email, createdUser.Id);

        return MapToDto(createdUser);
    }

    public async Task<UserDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.FetchingUserById(id);
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            _logger.UserNotFound(id);
            return null;
        }

        return MapToDto(user);
    }

    public async Task<(IEnumerable<UserDto> Users, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        _logger.FetchingAllUsers(page, pageSize);
        var allUsers = await _userRepository.GetAllAsync(cancellationToken);
        var totalCount = await _userRepository.GetTotalCountAsync(cancellationToken);

        var paginatedUsers = allUsers
            .OrderByDescending(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return (paginatedUsers.Select(MapToDto), (int)totalCount);
    }

    public async Task<UserDto> UpdateAsync(long id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        
        if (user is null)
        {
            _logger.UserNotFound(id);
            throw new KeyNotFoundException($"User with ID {id} not found");
        }

        // Update only provided fields
        if (!string.IsNullOrWhiteSpace(request.Name))
            user.Name = request.Name;

        if (request.Bio is not null)
            user.Bio = request.Bio;

        if (request.Location is not null)
            user.Location = request.Location;

        if (request.DateOfBirth.HasValue)
            user.DateOfBirth = request.DateOfBirth;

        if (!string.IsNullOrWhiteSpace(request.ProfileImageUrl))
            user.ProfileImageUrl = new Uri(request.ProfileImageUrl);

        if (!string.IsNullOrWhiteSpace(request.PreferredLanguage))
            user.PreferredLanguage = request.PreferredLanguage;

        var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
        return MapToDto(updatedUser);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.DeletingUser(id);
        var deleted = await _userRepository.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            _logger.UserNotFound(id);
            return false;
        }

        _logger.UserDeleted(id);
        return true;
    }

    public async Task<bool> ChangePasswordAsync(long id, ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        
        if (user is null)
        {
            _logger.UserNotFound(id);
            throw new KeyNotFoundException($"User with ID {id} not found");
        }

        if (!VerifyPassword(request.CurrentPassword, user.PasswordHash ?? string.Empty))
        {
            _logger.InvalidPasswordFormat(user.Email);
            throw new UnauthorizedAccessException("Current password is incorrect");
        }

        user.PasswordHash = HashPassword(request.NewPassword);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return true;
    }

    private string GenerateJwtToken(UserEntity user)
    {
        try
        {
            _logger.GeneratingJwtToken(user.Id);

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
            var issuer = jwtSettings["Issuer"] ?? "DevTrivia";
            var audience = jwtSettings["Audience"] ?? "DevTrivia.Users";
            var expirationMinutes = jwtSettings.GetValue<int>("ExpirationInMinutes", 60);

            var now = _timeProvider.GetUtcNow().UtcDateTime;
            var expiresAt = now.AddMinutes(expirationMinutes);

            var payload = new Dictionary<string, object>
            {
                // Standard JWT claims
                { "sub", user.Id.ToString() },
                { "iss", issuer },
                { "aud", audience },
                { "iat", new DateTimeOffset(now).ToUnixTimeSeconds() },
                { "exp", new DateTimeOffset(expiresAt).ToUnixTimeSeconds() },
                { "jti", Guid.NewGuid().ToString() },
                
                // ASP.NET Core Identity claims (for ClaimsPrincipal)
                { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id.ToString() },
                { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.Name },
                { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", user.Email },
                
                // Custom claims (optional)
                { "email", user.Email },
                { "name", user.Name }
            };

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var token = JWT.Encode(payload, keyBytes, JwsAlgorithm.HS256);

            return token;
        }
        catch (Exception ex)
        {
            _logger.JwtTokenGenerationFailed(user.Id, ex.Message, ex);
            throw;
        }
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch
        {
            return false;
        }
    }

    private static UserDto MapToDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
            Bio = user.Bio,
            Location = user.Location,
            DateOfBirth = user.DateOfBirth,
            LastLoginAt = user.LastLoginAt,
            PreferredLanguage = user.PreferredLanguage,
            CreatedAt = user.CreatedAt.GetValueOrDefault(),
            UpdatedAt = user.UpdatedAt
        };
    }
}

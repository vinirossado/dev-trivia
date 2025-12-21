using DevTrivia.API.Capabilities.User.Models;

namespace DevTrivia.API.Capabilities.User.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<UserDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<UserDto> Users, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<UserDto> UpdateAsync(long id, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ChangePasswordAsync(long id, ChangePasswordRequest request, CancellationToken cancellationToken = default);
}

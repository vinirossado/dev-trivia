using DevTrivia.API.Capabilities.User.Database.Entities;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.User.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly TriviaDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(TriviaDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.FetchingUserById(id);
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("fetching user by ID", ex.Message, ex);
            throw;
        }
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("fetching user by email", ex.Message, ex);
            throw;
        }
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.FetchingAllUsers(page, pageSize);
            return await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("fetching all users", ex.Message, ex);
            throw;
        }
    }

    public async Task<UserEntity> CreateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        try
        {
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.DatabaseOperationSuccess($"creating user with email {user.Email}");
            return user;
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("creating user", ex.Message, ex);
            throw;
        }
    }

    public async Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.UpdatingUser(user.Id);
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.UserUpdated(user.Id);
            return user;
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("updating user", ex.Message, ex);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.DeletingUser(id);
            var user = await _context.Users.FindAsync([id], cancellationToken);
            if (user is null)
            {
                _logger.UserNotFound(id);
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.UserDeleted(id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("deleting user", ex.Message, ex);
            throw;
        }
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if email exists", ex.Message, ex);
            throw;
        }
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Users.CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("getting total user count", ex.Message, ex);
            throw;
        }
    }
}

using DevTrivia.API.Capabilities.User.Database.Entities;

namespace DevTrivia.API.Capabilities.User.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<UserEntity> CreateAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

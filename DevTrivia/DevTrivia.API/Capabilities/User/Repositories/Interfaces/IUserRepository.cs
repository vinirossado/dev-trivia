namespace DevTrivia.API.Capabilities.User.Repositories.Interfaces;

public interface IUserRepository
{
    Task<Database.Entities.User?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Database.Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Database.Entities.User>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Database.Entities.User> CreateAsync(Database.Entities.User user, CancellationToken cancellationToken = default);
    Task<Database.Entities.User> UpdateAsync(Database.Entities.User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

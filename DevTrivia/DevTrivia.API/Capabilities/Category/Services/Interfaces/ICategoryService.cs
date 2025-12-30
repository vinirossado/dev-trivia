
namespace DevTrivia.API.Capabilities.Category.Services.Interfaces;

public interface ICategoryService
{
    Task<Database.Entities.Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Database.Entities.Category>> GetAll(CancellationToken cancellationToken = default);
    Task<Database.Entities.Category> CreateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default);
    Task<Database.Entities.Category> UpdateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Models;

namespace DevTrivia.API.Capabilities.Category.Services.Interfaces;

public interface ICategoryService
{
    Task<CategoryEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryEntity> CreateAsync(CategoryEntity category, CancellationToken cancellationToken = default);
    Task<CategoryEntity> UpdateAsync(CategoryRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

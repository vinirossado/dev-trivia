using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.Category.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<CategoryEntity>
{
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
}
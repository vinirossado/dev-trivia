using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Migrations;

namespace DevTrivia.API.Capabilities.Category.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly TriviaDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(TriviaDbContext context, ILogger<CategoryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    Task<Database.Entities.Category?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Database.Entities.Category>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Database.Entities.Category> CreateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Database.Entities.Category> UpdateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task<Database.Entities.Category?> ICategoryRepository.GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return GetByIdAsync(id, cancellationToken);
    }

    Task<IEnumerable<Database.Entities.Category>> ICategoryRepository.GetAll(CancellationToken cancellationToken)
    {
        return GetAll(cancellationToken);
    }
}

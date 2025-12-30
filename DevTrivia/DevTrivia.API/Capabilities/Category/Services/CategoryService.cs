using System.Security.Cryptography;
using System.Text;
using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Category.Services.Interfaces;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Jose;

namespace DevTrivia.API.Capabilities.Category.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeProvider _timeProvider;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ILogger<CategoryService> logger,
        IConfiguration configuration,
        TimeProvider timeProvider)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
        _configuration = configuration;
        _timeProvider = timeProvider;
    }

    public Task<Database.Entities.Category> CreateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Database.Entities.Category>> GetAll(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Database.Entities.Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Database.Entities.Category> UpdateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

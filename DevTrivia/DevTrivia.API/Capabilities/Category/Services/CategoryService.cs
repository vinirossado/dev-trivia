using Azure.Core;
using DevTrivia.API.Capabilities.Category.Models;
using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Category.Services.Interfaces;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Repositories;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Jose;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;

namespace DevTrivia.API.Capabilities.Category.Services;

public sealed class CategoryService : Interfaces.ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeProvider _timeProvider;
    private object _categoryService;

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

    public async Task<Database.Entities.Category> CreateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        if (await _categoryRepository.NameExistsAsync(category.Name, cancellationToken))
        {
            throw new InvalidOperationException("Category already registered");
        }
            await _categoryRepository.CreateAsync(category, cancellationToken);
            return category;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var categoryDb = await GetByIdAsync(id, cancellationToken);
        if (categoryDb == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        await _categoryRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Database.Entities.Category?>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _categoryRepository.GetAll(cancellationToken);
    }

    public async Task<Database.Entities.Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var getid = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (getid == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        return getid;
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Database.Entities.Category> UpdateAsync(CategoryRequest category, long id, CancellationToken cancellationToken = default)
    {
        var categoryDb = await GetByIdAsync(id, cancellationToken);
        if (categoryDb == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }
        categoryDb.Name = category.Name;
        categoryDb.Description = category.Description;
        return await _categoryRepository.UpdateAsync(categoryDb, cancellationToken);
    }
}

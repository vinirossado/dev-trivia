using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Models;
using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Category.Services.Interfaces;

namespace DevTrivia.API.Capabilities.Category.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<CategoryEntity> CreateAsync(CategoryEntity category, CancellationToken cancellationToken = default)
    {
        category.Name = category.Name.ToLower();

        if (await _categoryRepository.NameExistsAsync(category.Name, cancellationToken))
        {
            throw new InvalidOperationException("Category with this name already exists");
        }

        return await _categoryRepository.AddAsync(category, cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(id, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category with id {id} not found");
        }

        return await _categoryRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _categoryRepository.GetAllAsync(cancellationToken);
    }

    public async Task<CategoryEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found");
        }

        return category;
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _categoryRepository.GetTotalCountAsync(cancellationToken);
    }

    public async Task<CategoryEntity> UpdateAsync(CategoryRequest request, long id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found");
        }

        category.Name = request.Name.ToLower();
        category.Description = request.Description;

        return await _categoryRepository.UpdateAsync(category, cancellationToken);
    }
}
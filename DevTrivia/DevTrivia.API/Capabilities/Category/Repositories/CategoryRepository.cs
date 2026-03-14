using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.Category.Repositories;

public sealed class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
{
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(TriviaDbContext context, ILogger<CategoryRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(c => c.Name == name, cancellationToken);
    }
}
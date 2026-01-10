using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Database.Entities.Category?>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Database.Entities.Category> CreateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
         var categoryDb =  await _context.Categories.AddAsync(category, cancellationToken);
         await _context.SaveChangesAsync(cancellationToken);
         return categoryDb.Entity;
    }

    public async Task<Database.Entities.Category> UpdateAsync(Database.Entities.Category category, CancellationToken cancellationToken = default)
    {
        await _context.Categories.Where(c => c.Id == category.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.Name, category.Name)
                .SetProperty(c => c.Description, category.Description), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories.Where(c => c.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Database.Entities.Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Categories
                .AsNoTracking()
                .AnyAsync(u => u.Name == name, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if name exists", ex.Message, ex);
            throw;
        }
    }
}
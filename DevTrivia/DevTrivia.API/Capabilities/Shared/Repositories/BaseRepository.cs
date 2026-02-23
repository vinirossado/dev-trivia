using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.Shared.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly TriviaDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(TriviaDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public virtual async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.LongCountAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}

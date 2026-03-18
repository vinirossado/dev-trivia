using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.PlayerStats.Repositories;

public sealed class PlayerStatsRepository : BaseRepository<PlayerStatsEntity>, IPlayerStatsRepository
{
    private readonly ILogger<PlayerStatsRepository> _logger;

    public PlayerStatsRepository(TriviaDbContext context, ILogger<PlayerStatsRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<bool> NameExistsAsync(int Id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .AnyAsync(c => c.Id == Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if name exists", ex.Message, ex);
            throw;
        }
    }
    public async Task<bool> ExistsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .AnyAsync(x => x.UserId == userId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if stats exists", ex.Message, ex);
            throw;
        }
    }
    public async Task<PlayerStatsEntity?> StatsAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
            .Include(q => q.User)
            .FirstOrDefaultAsync(q => q.UserId == id);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if stats exists", ex.Message, ex);
            throw;
        }
    }
    public async Task<PlayerStatsEntity?> UpdateAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
            .Include(q => q.User)
            .FirstOrDefaultAsync(q => q.UserId == id);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if stats exists", ex.Message, ex);
            throw;
        }
    }
}
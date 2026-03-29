using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.PlayerStats.Repositories;

public sealed class PlayerStatsRepository : BaseRepository<PlayerStatsEntity>, IPlayerStatsRepository
{

    public PlayerStatsRepository(TriviaDbContext context) : base(context)
    {
    }

    public async Task<PlayerStatsEntity?> GetStatsByUserIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(q => q.User)
            .FirstOrDefaultAsync(q => q.UserId == id, cancellationToken);
    }
}
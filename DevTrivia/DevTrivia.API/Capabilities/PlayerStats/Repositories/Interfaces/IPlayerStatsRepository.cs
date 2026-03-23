using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;

public interface IPlayerStatsRepository : IRepository<PlayerStatsEntity>
{
    Task<PlayerStatsEntity?> GetStatsByUserIdAsync(long id, CancellationToken cancellationToken = default);
}

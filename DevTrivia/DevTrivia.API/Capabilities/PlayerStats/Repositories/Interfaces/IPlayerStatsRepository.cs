using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;

public interface IPlayerStatsRepository : IRepository<PlayerStatsEntity>
{
    Task<bool> NameExistsAsync(int Id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity?> StatsAsync(long id, CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity?> UpdateAsync(long id, CancellationToken cancellationToken = default);
}

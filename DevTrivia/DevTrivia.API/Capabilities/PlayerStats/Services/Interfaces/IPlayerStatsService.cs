using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Models;

namespace DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;

public interface IPlayerStatsService
{
    Task<PlayerStatsEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity?> GetStatsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlayerStatsEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity> CreateAsync(PlayerStatsRequest request, CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity> UpdateAsync(PlayerStatsRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

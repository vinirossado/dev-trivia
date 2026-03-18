using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Models;

namespace DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;

public interface IPlayerStatsService
{
    Task<PlayerStatsEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlayerStatsEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity> CreateAsync(PlayerStatsEntity playerstats, CancellationToken cancellationToken = default);
    Task<PlayerStatsEntity> UpdateAsync(PlayerStatsRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default);
}

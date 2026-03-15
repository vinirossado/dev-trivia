using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Models;

namespace DevTrivia.API.Capabilities.Match.Services.Interfaces;

public interface IMatchService
{
    Task<MatchEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MatchEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MatchEntity> CreateAsync(MatchRequest request, CancellationToken cancellationToken = default);
    Task<MatchEntity> UpdateAsync(MatchRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default);
}
using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.PlayerAnswer.Repositories.Interfaces;

public interface IPlayerAnswerRepository : IRepository<PlayerAnswerEntity>
{
    Task<IEnumerable<PlayerAnswerEntity>> GetByMatchIdAsync(long matchId, CancellationToken cancellationToken = default);
    Task<PlayerAnswerEntity?> GetUnansweredByMatchIdAsync(long matchId, CancellationToken cancellationToken = default);
    Task<PlayerAnswerEntity?> GetByMatchAndQuestionAsync(long matchId, long questionId, CancellationToken cancellationToken = default);
}
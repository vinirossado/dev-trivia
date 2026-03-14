using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using DevTrivia.API.Capabilities.PlayerAnswer.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.PlayerAnswer.Repositories;

public sealed class PlayerAnswerRepository : BaseRepository<PlayerAnswerEntity>, IPlayerAnswerRepository
{
    public PlayerAnswerRepository(TriviaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PlayerAnswerEntity>> GetByMatchIdAsync(long matchId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(pa => pa.Question)
            .Include(pa => pa.SelectedAnswerOption)
            .Where(pa => pa.MatchId == matchId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PlayerAnswerEntity?> GetUnansweredByMatchIdAsync(long matchId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(pa => pa.Question)
            .Where(pa => pa.MatchId == matchId && pa.AnsweredAt == default)
            .OrderBy(pa => pa.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PlayerAnswerEntity?> GetByMatchAndQuestionAsync(long matchId, long questionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(pa => pa.MatchId == matchId && pa.QuestionId == questionId, cancellationToken);
    }
}

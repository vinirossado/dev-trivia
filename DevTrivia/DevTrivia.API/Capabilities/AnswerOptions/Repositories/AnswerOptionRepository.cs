using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.AnswerOptions.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.AnswerOptions.Repositories;

public class AnswerOptionRepository : BaseRepository<AnswerOptionEntity>, IAnswerOptionRepository
{
    public AnswerOptionRepository(TriviaDbContext context) : base(context)
    {
    }

    public override async Task<AnswerOptionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet.Include(x => x.QuestionEntity)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<AnswerOptionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.QuestionEntity)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AnswerOptionEntity>> GetAnswerOptionsByQuestionId(long questionId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(x => x.QuestionEntity)
            .Where(x => x.QuestionId == questionId)
            .ToListAsync(cancellationToken);
    }
}
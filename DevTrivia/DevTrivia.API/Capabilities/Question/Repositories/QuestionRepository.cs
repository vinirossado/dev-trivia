using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.Question.Repositories;

public sealed class QuestionRepository : BaseRepository<QuestionEntity>, IQuestionRepository
{
    private readonly ILogger<QuestionRepository> _logger;

    public QuestionRepository(TriviaDbContext context, ILogger<QuestionRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<bool> TitleExistsAsync(string title, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .AnyAsync(q => q.Title == title, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if question title exists", ex.Message, ex);
            throw;
        }
    }

    public override async Task<IEnumerable<QuestionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(q => q.Category)
            .ToListAsync(cancellationToken);
    }

    public override async Task<QuestionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(q => q.Category)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }
}

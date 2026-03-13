using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Enums;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
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
        return await DbSet
            .AsNoTracking()
            .AnyAsync(q => q.Title == title, cancellationToken);
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

    public async Task<IEnumerable<QuestionEntity>> GetByCategoryIdAsync(long categoryId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(q => q.Category)
            .Where(q => q.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<QuestionEntity>> GetByCategoryAndDifficultyAsync(
        long categoryId,
        DifficultyEnum difficulty,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(q => q.Category)
            .Where(q => q.CategoryId == categoryId && q.Difficulty == difficulty)
            .ToListAsync(cancellationToken);
    }
}
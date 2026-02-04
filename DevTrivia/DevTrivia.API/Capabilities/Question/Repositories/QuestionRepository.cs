using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.Question.Repositories;

public sealed class QuestionRepository : IQuestionRepository
{
    private readonly TriviaDbContext _context;
    private readonly ILogger<QuestionRepository> _logger;

    public QuestionRepository(TriviaDbContext context, ILogger<QuestionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Database.Entities.Question?>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Questions.ToListAsync(cancellationToken);
    }

    public async Task<Database.Entities.Question> CreateAsync(Database.Entities.Question question, CancellationToken cancellationToken = default)
    {
        var questionDb = await _context.Questions.AddAsync(question, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return questionDb.Entity;
    }

    public async Task<Database.Entities.Question> UpdateAsync(Database.Entities.Question question, CancellationToken cancellationToken = default)
    {
        await _context.Questions.Where(c => c.Id == question.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.Title, question.Title)
                .SetProperty(c => c.Description, question.Description), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return question;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var category = await _context.Questions.Where(c => c.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Database.Entities.Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _context.Questions.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> NameExistsAsync(string title, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Questions
                .AsNoTracking()
                .AnyAsync(u => u.Title == title, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if name exists", ex.Message, ex);
            throw;
        }
    }
}
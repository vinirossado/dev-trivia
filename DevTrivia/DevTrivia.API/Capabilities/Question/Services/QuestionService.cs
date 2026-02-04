using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;

namespace DevTrivia.API.Capabilities.Question.Services;

public sealed class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ILogger<QuestionService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeProvider _timeProvider;

    public QuestionService(
        IQuestionRepository questionRepository,
        ILogger<QuestionService> logger,
        IConfiguration configuration,
        TimeProvider timeProvider)
    {
        _questionRepository = questionRepository;
        _logger = logger;
        _configuration = configuration;
        _timeProvider = timeProvider;
    }

    public async Task<Question.Database.Entities.Question> CreateAsync(QuestionRequest request, CancellationToken cancellationToken = default)
    {
        if (await _questionRepository.NameExistsAsync(request.Title, cancellationToken))
        {
            throw new InvalidOperationException("Category already registered");
        }

        var question = new Question.Database.Entities.Question
        {
            CategoryId = request.CategoryId,
            Title = request.Title.ToLower(),
            Description = request.Description,
            Difficulty = request.Difficulty
        };

        await _questionRepository.CreateAsync(question, cancellationToken);

        return question;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var questionDb = await GetByIdAsync(id, cancellationToken);
        if (questionDb == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }

        await _questionRepository.DeleteAsync(id, cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Question.Database.Entities.Question?>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _questionRepository.GetAll(cancellationToken);
    }

    public async Task<Question.Database.Entities.Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(id, cancellationToken);
        if (question == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }

        return question;
    }

    public async Task<Question.Database.Entities.Question> UpdateAsync(QuestionRequest question, long id, CancellationToken cancellationToken = default)
    {
        var questionDb = await GetByIdAsync(id, cancellationToken);
        if (questionDb == null)
        {
            throw new KeyNotFoundException($"Category with id {id} not found.");
        }

        questionDb.Title = question.Title;
        questionDb.Description = question.Description;
        return await _questionRepository.UpdateAsync(questionDb, cancellationToken);
    }
}
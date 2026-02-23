using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;

namespace DevTrivia.API.Capabilities.Question.Services;

public sealed class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(
        IQuestionRepository questionRepository,
        ILogger<QuestionService> logger)
    {
        _questionRepository = questionRepository;
        _logger = logger;
    }

    public async Task<QuestionEntity> CreateAsync(QuestionRequest request, CancellationToken cancellationToken = default)
    {
        if (await _questionRepository.TitleExistsAsync(request.Title, cancellationToken))
        {
            throw new InvalidOperationException("Question with this title already exists");
        }

        var question = new QuestionEntity
        {
            CategoryId = request.CategoryId,
            Title = request.Title,
            Description = request.Description,
            Difficulty = request.Difficulty.ToLower()
        };

        return await _questionRepository.AddAsync(question, cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var questionExists = await _questionRepository.ExistsAsync(id, cancellationToken);
        if (!questionExists)
        {
            throw new KeyNotFoundException($"Question with id {id} not found");
        }

        return await _questionRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<QuestionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _questionRepository.GetAllAsync(cancellationToken);
    }

    public async Task<QuestionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(id, cancellationToken);
        if (question == null)
        {
            throw new KeyNotFoundException($"Question with id {id} not found");
        }

        return question;
    }

    public async Task<QuestionEntity> UpdateAsync(QuestionRequest request, long id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(id, cancellationToken);
        if (question == null)
        {
            throw new KeyNotFoundException($"Question with id {id} not found");
        }

        question.Title = request.Title;
        question.Description = request.Description;
        question.Difficulty = request.Difficulty.ToLower();
        question.CategoryId = request.CategoryId;
        
        return await _questionRepository.UpdateAsync(question, cancellationToken);
    }
}

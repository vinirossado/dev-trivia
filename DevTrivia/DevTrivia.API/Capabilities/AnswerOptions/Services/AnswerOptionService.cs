using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.AnswerOptions.Extensions;
using DevTrivia.API.Capabilities.AnswerOptions.Models;
using DevTrivia.API.Capabilities.AnswerOptions.Repositories.Interfaces;
using DevTrivia.API.Capabilities.AnswerOptions.Services.Interfaces;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;

namespace DevTrivia.API.Capabilities.AnswerOptions.Services;

public sealed class AnswerOptionService : IAnswerOptionService
{
    private readonly IAnswerOptionRepository _answerOptionRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ILogger<AnswerOptionService> _logger;

    public AnswerOptionService(
        IAnswerOptionRepository answerOptionRepository,
        IQuestionRepository questionRepository,
        ILogger<AnswerOptionService> logger)
    {
        _answerOptionRepository = answerOptionRepository;
        _questionRepository = questionRepository;
        _logger = logger;
    }

    public async Task<AnswerOptionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var option = await _answerOptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Answer option with id {id} not found");

        return option;
    }

    public async Task<IEnumerable<AnswerOptionEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _answerOptionRepository.GetAllAsync(cancellationToken);
    }

    public async Task<IEnumerable<AnswerOptionEntity>> GetAnswerOptionsByQuestionId(long questionId, CancellationToken cancellationToken = default)
    {
        var questionExists = await _questionRepository.ExistsAsync(questionId, cancellationToken);
        if (!questionExists)
        {
            throw new KeyNotFoundException($"Question with id {questionId} not found");
        }

        return await _answerOptionRepository.GetAnswerOptionsByQuestionId(questionId, cancellationToken);
    }

    public async Task<AnswerOptionEntity> CreateAsync(AnswerOptionRequest request, CancellationToken cancellationToken = default)
    {
        var questionExists = await _questionRepository.ExistsAsync(request.QuestionId, cancellationToken);
        if (!questionExists)
        {
            throw new KeyNotFoundException($"Question with id {request.QuestionId} not found");
        }

        var existing = await _answerOptionRepository.GetAnswerOptionsByQuestionId(request.QuestionId, cancellationToken);
        var existingList = existing.ToList();

        if (existingList.Count >= 4)
        {
            throw new InvalidOperationException("A question can have at most 4 answer options");
        }

        if (request.IsCorrect && existingList.Any(a => a.IsCorrect))
        {
            throw new InvalidOperationException("A correct answer already exists for this question");
        }

        var entity = request.ToEntity();
        return await _answerOptionRepository.AddAsync(entity, cancellationToken);
    }

    public async Task<AnswerOptionEntity> UpdateAsync(AnswerOptionRequest request, long id, CancellationToken cancellationToken = default)
    {
        var option = await _answerOptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Answer option with id {id} not found");

        if (option.QuestionId != request.QuestionId)
        {
            var questionExists = await _questionRepository.ExistsAsync(request.QuestionId, cancellationToken);
            if (!questionExists)
            {
                throw new KeyNotFoundException($"Question with id {request.QuestionId} not found");
            }
        }

        if (request.IsCorrect && !option.IsCorrect)
        {
            var existing = await _answerOptionRepository.GetAnswerOptionsByQuestionId(request.QuestionId, cancellationToken);
            if (existing.Any(a => a.IsCorrect && a.Id != id))
            {
                throw new InvalidOperationException("A correct answer already exists for this question");
            }
        }

        option.Text = request.Text;
        option.IsCorrect = request.IsCorrect;
        option.QuestionId = request.QuestionId;

        return await _answerOptionRepository.UpdateAsync(option, cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var exists = await _answerOptionRepository.ExistsAsync(id, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Answer option with id {id} not found");
        }

        return await _answerOptionRepository.DeleteAsync(id, cancellationToken);
    }
}
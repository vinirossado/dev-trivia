using Azure.Core;
using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;
using DevTrivia.API.Capabilities.User.Models;
using DevTrivia.API.Capabilities.User.Repositories;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Capabilities.User.Services.Interfaces;
using DevTrivia.API.Infrastructure.Logging;
using Jose;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DevTrivia.API.Capabilities.Questions.Services;

public sealed class QuestionService : Question.Services.Interfaces.IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ILogger<QuestionService> _logger;
    private readonly IConfiguration _configuration;
    private readonly TimeProvider _timeProvider;
    private object _questionService;

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
    public async Task<Question.Database.Entities.Question> CreateAsync(Question.Database.Entities.Question question, CancellationToken cancellationToken = default)
    {
        if (await _questionRepository.NameExistsAsync(question.Title, cancellationToken))
        {
            throw new InvalidOperationException("Category already registered");
        }
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

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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

    Task<Question.Database.Entities.Question?> IQuestionService.GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Question.Database.Entities.Question> IQuestionService.UpdateAsync(QuestionRequest question, long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

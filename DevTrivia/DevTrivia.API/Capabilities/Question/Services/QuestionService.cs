﻿﻿﻿using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Enums;
using DevTrivia.API.Capabilities.Question.Extensions;
using DevTrivia.API.Capabilities.Question.Models;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Question.Services.Interfaces;

namespace DevTrivia.API.Capabilities.Question.Services;

public sealed class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(
        IQuestionRepository questionRepository,
        ICategoryRepository categoryRepository,
        ILogger<QuestionService> logger)
    {
        _questionRepository = questionRepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<QuestionEntity> CreateAsync(QuestionRequest request, CancellationToken cancellationToken = default)
    {
        // Validate category exists
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category with id {request.CategoryId} not found");
        }

        if (await _questionRepository.TitleExistsAsync(request.Title, cancellationToken))
        {
            throw new InvalidOperationException("Question with this title already exists");
        }

        var question = request.ToEntity();

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

    public async Task<IEnumerable<QuestionEntity>> GetByCategoryIdAsync(long categoryId, CancellationToken cancellationToken = default)
    {
        // Validate category exists
        var categoryExists = await _categoryRepository.ExistsAsync(categoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category with id {categoryId} not found");
        }

        var questions = await _questionRepository.GetByCategoryIdAsync(categoryId, cancellationToken);
        
        // Return empty list is OK - category exists but has no questions
        return questions;
    }

    public async Task<IEnumerable<QuestionEntity>> GetByCategoryAndDifficultyAsync(
        long categoryId, 
        DifficultyEnum difficulty, 
        CancellationToken cancellationToken = default)
    {
        // Validate category exists
        var categoryExists = await _categoryRepository.ExistsAsync(categoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category with id {categoryId} not found");
        }

        var questions = await _questionRepository.GetByCategoryAndDifficultyAsync(categoryId, difficulty, cancellationToken);
        
        // Return empty list is OK - category exists but has no questions with that difficulty
        return questions;
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

        // Validate category exists if it's being changed
        if (question.CategoryId != request.CategoryId)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                throw new KeyNotFoundException($"Category with id {request.CategoryId} not found");
            }
        }

        question.Title = request.Title;
        question.Description = request.Description;
        question.Difficulty = request.Difficulty;
        question.CategoryId = request.CategoryId;
        
        return await _questionRepository.UpdateAsync(question, cancellationToken);
    }
}

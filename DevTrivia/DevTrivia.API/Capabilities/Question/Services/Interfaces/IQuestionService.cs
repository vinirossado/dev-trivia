﻿using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Enums;
using DevTrivia.API.Capabilities.Question.Models;

namespace DevTrivia.API.Capabilities.Question.Services.Interfaces;

public interface IQuestionService
{
    Task<QuestionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<QuestionEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<QuestionEntity>> GetByCategoryIdAsync(long categoryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<QuestionEntity>> GetByCategoryAndDifficultyAsync(long categoryId, DifficultyEnum difficulty, CancellationToken cancellationToken = default);
    Task<QuestionEntity> CreateAsync(QuestionRequest request, CancellationToken cancellationToken = default);
    Task<QuestionEntity> UpdateAsync(QuestionRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}

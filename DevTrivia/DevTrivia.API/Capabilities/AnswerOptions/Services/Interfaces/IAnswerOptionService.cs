using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.AnswerOptions.Models;

namespace DevTrivia.API.Capabilities.AnswerOptions.Services.Interfaces;

public interface IAnswerOptionService
{
    Task<AnswerOptionEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AnswerOptionEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AnswerOptionEntity>> GetAnswerOptionsByQuestionId(long questionId, CancellationToken cancellationToken = default);
    Task<AnswerOptionEntity> CreateAsync(AnswerOptionRequest request, CancellationToken cancellationToken = default);
    Task<AnswerOptionEntity> UpdateAsync(AnswerOptionRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
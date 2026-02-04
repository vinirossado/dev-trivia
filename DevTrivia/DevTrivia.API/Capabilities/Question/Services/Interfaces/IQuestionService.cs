using DevTrivia.API.Capabilities.Question.Models;

namespace DevTrivia.API.Capabilities.Question.Services.Interfaces;

public interface IQuestionService
{
    Task<Database.Entities.Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Database.Entities.Question?>> GetAll(CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> CreateAsync(QuestionRequest request, CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> UpdateAsync(QuestionRequest request, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
using DevTrivia.API.Capabilities.Question.Models;

namespace DevTrivia.API.Capabilities.Question.Services.Interfaces;

public interface IQuestionService
{
    Task<Database.Entities.Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Database.Entities.Question?>> GetAll(CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> CreateAsync(Database.Entities.Question question, CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> UpdateAsync(QuestionRequest question, long id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
}
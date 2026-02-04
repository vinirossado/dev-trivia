namespace DevTrivia.API.Capabilities.Question.Repositories.Interfaces;

public interface IQuestionRepository
{
    Task<Database.Entities.Question?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Database.Entities.Question?>> GetAll(CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> CreateAsync(Database.Entities.Question question, CancellationToken cancellationToken = default);
    Task<Database.Entities.Question> UpdateAsync(Database.Entities.Question question, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string title, CancellationToken cancellationToken);
}

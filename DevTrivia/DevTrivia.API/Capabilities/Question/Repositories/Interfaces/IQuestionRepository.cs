using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.Question.Repositories.Interfaces;

public interface IQuestionRepository : IRepository<QuestionEntity>
{
    Task<bool> TitleExistsAsync(string title, CancellationToken cancellationToken = default);
}
using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Repositories;

namespace DevTrivia.API.Capabilities.AnswerOptions.Repositories.Interfaces;

public interface IAnswerOptionRepository : IRepository<AnswerOptionEntity>
{
    Task<IEnumerable<AnswerOptionEntity>> GetAnswerOptionsByQuestionId(long questionId, CancellationToken cancellationToken = default);
}
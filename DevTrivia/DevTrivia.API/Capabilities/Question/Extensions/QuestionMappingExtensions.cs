using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Models;

namespace DevTrivia.API.Capabilities.Question.Extensions;

public static class QuestionMappingExtensions
{
    public static QuestionResponse ToResponse(this QuestionEntity entity)
    {
        return new QuestionResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Difficulty = entity.Difficulty,
            CategoryId = entity.CategoryId,
            CategoryName = entity.Category?.Name,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static QuestionEntity ToEntity(this QuestionRequest request)
    {
        return new QuestionEntity
        {
            Title = request.Title,
            Description = request.Description,
            Difficulty = request.Difficulty,
            CategoryId = request.CategoryId
        };
    }

    public static IEnumerable<QuestionResponse> ToResponseList(this IEnumerable<QuestionEntity> entities)
    {
        return entities.Select(e => e.ToResponse());
    }
}

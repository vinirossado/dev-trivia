using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.AnswerOptions.Models;

namespace DevTrivia.API.Capabilities.AnswerOptions.Extensions;

public static class AnswerOptionMappingExtensions
{
    public static AnswerOptionResponse ToResponse(this AnswerOptionEntity entity)
    {
        return new AnswerOptionResponse
        {
            Id = entity.Id,
            Text = entity.Text,
            IsCorrect = entity.IsCorrect,
            QuestionId = entity.QuestionId,
            QuestionTitle = entity.QuestionEntity?.Title,
        };
    }

    public static AnswerOptionEntity ToEntity(this AnswerOptionRequest request)
    {
        return new AnswerOptionEntity
        {
            Text = request.Text,
            IsCorrect = request.IsCorrect,
            QuestionId = request.QuestionId,
        };
    }

    public static IEnumerable<AnswerOptionResponse> ToResponseList(this IEnumerable<AnswerOptionEntity> entities)
    {
        return entities.Select(e => e.ToResponse());
    }
}
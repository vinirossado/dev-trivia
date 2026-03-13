using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Models;

namespace DevTrivia.API.Capabilities.Match.Extensions;

public static class MatchMappingExtensions
{
    public static MatchResponse ToResponse(this MatchEntity entity)
    {
        return new MatchResponse
        {
            Id = entity.Id,
            StartedAt = entity.StartedAt,
            EndedAt = entity.EndedAt,
            Status = entity.Status,
            SelectedCategoryId = entity.SelectedCategoryId,
            CategoryName = entity.Category?.Name
        };
    }

    public static MatchEntity ToEntity(this MatchRequest request)
    {
        return new MatchEntity
        {
            StartedAt = request.StartedAt,
            EndedAt = request.EndedAt,
            Status = request.Status,
            SelectedCategoryId = request.SelectedCategoryId
        };
    }

    public static IEnumerable<MatchResponse> ToResponseList(this IEnumerable<MatchEntity> entities)
    {
        return entities.Select(e => e.ToResponse());
    }
}

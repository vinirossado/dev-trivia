using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Models;

namespace DevTrivia.API.Capabilities.PlayerStats.Extensions;

public static class PlayerStatsMappingExtensions
{
    public static PlayerStatsResponse ToResponse(this PlayerStatsEntity entity)
    {
        return new PlayerStatsResponse
        {
            UserId = entity.UserId,
            TotalMatches = entity.TotalMatches,
            TotalCorrect = entity.TotalCorrect,
            EloRating = entity.EloRating
        };
    }

    public static PlayerStatsEntity ToEntity(this PlayerStatsRequest request)
    {
        return new PlayerStatsEntity
        {
            UserId = request.UserId,
            TotalCorrect = request.TotalCorrect,
            EloRating = request.EloRating,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static IEnumerable<PlayerStatsResponse> ToResponseList(this IEnumerable<PlayerStatsEntity> entities)
    {
        return entities.Select(e => e.ToResponse());
    }
}

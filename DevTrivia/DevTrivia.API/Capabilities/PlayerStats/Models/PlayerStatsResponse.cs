using DevTrivia.API.Capabilities.PlayerStats.Enums;

namespace DevTrivia.API.Capabilities.PlayerStats.Models;

public record PlayerStatsResponse
{
    public long UserId { get; init; }
    public long TotalMatches { get; init; }
    public long TotalCorrect { get; init; }
    public EloRating EloRating { get; init; }
}

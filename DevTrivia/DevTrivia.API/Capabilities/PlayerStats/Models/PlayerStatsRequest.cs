using DevTrivia.API.Capabilities.PlayerStats.Enums;

namespace DevTrivia.API.Capabilities.PlayerStats.Models;

public record PlayerStatsRequest
{
    public required long UserId { get; init; }
    public required int TotalCorrect { get; init; }
    public required EloRating EloRating { get; init; }
}

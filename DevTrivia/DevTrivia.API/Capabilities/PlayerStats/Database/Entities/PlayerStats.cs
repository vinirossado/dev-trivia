using DevTrivia.API.Capabilities.PlayerStats.Enums;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Capabilities.User.Database.Entities;

namespace DevTrivia.API.Capabilities.PlayerStats.Database.Entities;

public sealed class PlayerStatsEntity : BaseEntity
{
    public required long UserId { get; set; }
    public int TotalMatches { get; set; } = 1;
    public int TotalCorrect { get; set; }
    public EloRating EloRating { get; set; }

    public UserEntity User { get; set; } = null!;
}

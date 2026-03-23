using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Capabilities.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace DevTrivia.API.Capabilities.User.Database.Entities;

public sealed class UserEntity : BaseEntity
{
    public RoleEnum Role { get; set; } = RoleEnum.Player;
    public ICollection<PlayerStatsEntity> PlayerStats { get; } = new List<PlayerStatsEntity>();
    public ICollection<MatchEntity> Matches { get; } = new List<MatchEntity>();

    public required string Name { get; set; } = string.Empty;

    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    public string? PasswordHash { get; set; }

    public Uri? ProfileImageUrl { get; set; }

    public string AuthProvider { get; set; } = "local"; // local, google, github, microsoft

    public string? ExternalId { get; set; } // ID from OAuth provider (null for local auth)

    public string? Bio { get; set; }

    public string? Location { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public string? PreferredLanguage { get; set; }
}
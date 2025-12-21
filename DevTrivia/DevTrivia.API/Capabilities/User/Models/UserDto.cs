namespace DevTrivia.API.Capabilities.User.Models;

public sealed record UserDto
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public Uri? ProfileImageUrl { get; init; }
    public string? Bio { get; init; }
    public string? Location { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public DateTime? LastLoginAt { get; init; }
    public string? PreferredLanguage { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

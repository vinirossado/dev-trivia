using DevTrivia.API.Capabilities.User.Enums;

namespace DevTrivia.API.Capabilities.User.Models;

public sealed record LoginResponse
{
    public required string Token { get; init; }
    public required string TokenType { get; init; } = "Bearer";
    public required long UserId { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required RoleEnum Role { get; init; }
    public required DateTime ExpiresAt { get; init; }
}

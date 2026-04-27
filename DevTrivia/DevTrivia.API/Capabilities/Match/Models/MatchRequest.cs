
using DevTrivia.API.Capabilities.Match.Enums;

namespace DevTrivia.API.Capabilities.Match.Models;

public record MatchRequest
{
    public required StatusEnum Status { get; init; }
    public required long UserId { get; init; }
    public required long SelectedCategoryId { get; init; }
    public bool IsComputed { get; internal set; }
}
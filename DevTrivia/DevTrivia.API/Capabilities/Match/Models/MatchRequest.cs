
using DevTrivia.API.Capabilities.Match.Enums;

namespace DevTrivia.API.Capabilities.Match.Models;

public record MatchRequest
{
    public required DateTime StartedAt { get; init; }
    public required DateTime EndedAt { get; init; }
    public required StatusEnum Status { get; init; }
    public required long SelectedCategoryId { get; init; }
}

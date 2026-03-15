using DevTrivia.API.Capabilities.Match.Enums;

namespace DevTrivia.API.Capabilities.Match.Models;

public record MatchResponse
{
    public long Id { get; init; }
    public StatusEnum Status { get; init; }
    public long SelectedCategoryId { get; init; }
    public string? CategoryName { get; init; }
}
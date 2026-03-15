using DevTrivia.API.Capabilities.Match.Enums;

namespace DevTrivia.API.Capabilities.Match.Models;

public record GameStartResponse
{
    public long MatchId { get; init; }
    public int TotalQuestions { get; init; }
    public string CategoryName { get; init; } = null!;
    public StatusEnum Status { get; init; }
}
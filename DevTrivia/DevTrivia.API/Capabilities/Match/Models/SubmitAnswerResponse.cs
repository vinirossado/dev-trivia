namespace DevTrivia.API.Capabilities.Match.Models;

public record SubmitAnswerResponse
{
    public bool IsCorrect { get; init; }
    public long CorrectAnswerOptionId { get; init; }
    public bool IsLastQuestion { get; init; }
}

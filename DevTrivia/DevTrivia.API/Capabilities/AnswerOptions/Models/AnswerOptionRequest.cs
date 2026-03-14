namespace DevTrivia.API.Capabilities.AnswerOptions.Models;

public record AnswerOptionRequest
{
    public required string Text { get; init; }
    public required bool IsCorrect { get; init; }
    public long QuestionId { get; init; }
}
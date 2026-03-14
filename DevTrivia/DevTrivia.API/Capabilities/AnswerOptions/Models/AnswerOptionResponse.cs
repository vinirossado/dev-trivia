namespace DevTrivia.API.Capabilities.AnswerOptions.Models;

public record AnswerOptionResponse
{
    public long Id { get; init; }
    public string Text { get; init; } = null!;
    public bool IsCorrect { get; init; }
    public long QuestionId { get; init; }
    public string? QuestionTitle { get; init; }
}
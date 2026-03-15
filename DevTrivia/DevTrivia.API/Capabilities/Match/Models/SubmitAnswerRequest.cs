namespace DevTrivia.API.Capabilities.Match.Models;

public record SubmitAnswerRequest
{
    public required long QuestionId { get; init; }
    public long? SelectedAnswerOptionId { get; init; }
}
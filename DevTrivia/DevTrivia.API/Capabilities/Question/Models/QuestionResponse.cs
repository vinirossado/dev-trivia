namespace DevTrivia.API.Capabilities.Question.Models;

public record QuestionResponse
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Difficulty { get; init; } = string.Empty;
    public long CategoryId { get; init; }
    public string? CategoryName { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
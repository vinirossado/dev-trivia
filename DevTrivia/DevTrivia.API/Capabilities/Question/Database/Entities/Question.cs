using DevTrivia.API.Capabilities.Trivia.Models;

namespace DevTrivia.API.Capabilities.Question.Database.Entities;

public sealed class Question : BaseEntity
{
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required string Difficulty { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
}
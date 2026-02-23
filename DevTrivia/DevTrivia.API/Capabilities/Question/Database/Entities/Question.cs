using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Capabilities.Category.Database.Entities;

namespace DevTrivia.API.Capabilities.Question.Database.Entities;

public sealed class QuestionEntity : BaseEntity
{
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required string Difficulty { get; set; } = string.Empty;

    public long CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
}
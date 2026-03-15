using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Question.Enums;
using DevTrivia.API.Capabilities.Shared.Models;

namespace DevTrivia.API.Capabilities.Question.Database.Entities;

public sealed class QuestionEntity : BaseEntity
{
    public required string Title { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required DifficultyEnum Difficulty { get; set; }

    public long CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    public ICollection<AnswerOptionEntity> AnswerOptions { get; set; } = [];
}
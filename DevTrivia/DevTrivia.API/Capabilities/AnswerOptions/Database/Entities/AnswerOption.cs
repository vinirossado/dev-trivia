using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;

namespace DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;

public class AnswerOptionEntity : BaseEntity
{
    public long QuestionId { get; set; }
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }

    public QuestionEntity QuestionEntity { get; set; } = null!;

}
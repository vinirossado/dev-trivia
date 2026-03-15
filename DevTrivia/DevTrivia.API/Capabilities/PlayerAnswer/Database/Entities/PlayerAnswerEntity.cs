using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;

namespace DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;

public class PlayerAnswerEntity : BaseEntity
{
    public long MatchId { get; set; }
    public MatchEntity Match { get; set; } = null!;

    public long QuestionId { get; set; }
    public QuestionEntity Question { get; set; } = null!;

    public long? SelectedAnswerOptionId { get; set; }
    public AnswerOptionEntity? SelectedAnswerOption { get; set; }

    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }
}
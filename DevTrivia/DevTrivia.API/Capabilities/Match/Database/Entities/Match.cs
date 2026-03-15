using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;

namespace DevTrivia.API.Capabilities.Match.Database.Entities;

public sealed class MatchEntity : BaseEntity
{
    public StatusEnum Status { get; set; }

    public long SelectedCategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    public ICollection<PlayerAnswerEntity> PlayerAnswers { get; set; } = [];
}
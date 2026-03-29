using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;
using DevTrivia.API.Capabilities.User.Database.Entities;

namespace DevTrivia.API.Capabilities.Match.Database.Entities;

public sealed class MatchEntity : BaseEntity
{
    public StatusEnum Status { get; set; }
    public required long UserId { get; set; }
    public long SelectedCategoryId { get; set; }

    public CategoryEntity Category { get; set; } = null!;
    public UserEntity User { get; set; } = null!;
    public ICollection<PlayerAnswerEntity> PlayerAnswers { get; set; } = [];
}
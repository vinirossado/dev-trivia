using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Shared.Models;

namespace DevTrivia.API.Capabilities.Category.Database.Entities;

public sealed class CategoryEntity : BaseEntity
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;

    public ICollection<QuestionEntity> Questions { get; } = new List<QuestionEntity>();
    public ICollection<MatchEntity> Matches { get; } = new List<MatchEntity>();
}
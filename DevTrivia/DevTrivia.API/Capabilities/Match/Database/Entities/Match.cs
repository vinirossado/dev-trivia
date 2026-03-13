using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Enums;
using DevTrivia.API.Capabilities.Shared.Models;
using Microsoft.VisualBasic;

namespace DevTrivia.API.Capabilities.Match.Database.Entities;

public sealed class MatchEntity : BaseEntity
{
    public required DateTime StartedAt { get; set; }
    public required DateTime EndedAt { get; set; }
    public StatusEnum Status { get; set; }

    public long SelectedCategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
}

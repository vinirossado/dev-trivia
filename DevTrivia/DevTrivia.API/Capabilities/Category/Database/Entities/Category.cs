using DevTrivia.API.Capabilities.Trivia.Models;

namespace DevTrivia.API.Capabilities.Category.Database.Entities;

public sealed class Category : BaseEntity
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;


}
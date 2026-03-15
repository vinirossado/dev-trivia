namespace DevTrivia.API.Capabilities.Category.Models;

public record CategoryRequest
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
}
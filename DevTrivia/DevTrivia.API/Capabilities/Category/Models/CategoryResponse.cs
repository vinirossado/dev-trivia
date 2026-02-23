namespace DevTrivia.API.Capabilities.Category.Models;

public record CategoryResponse
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}

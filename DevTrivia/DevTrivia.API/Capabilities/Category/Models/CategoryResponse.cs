namespace DevTrivia.API.Capabilities.Category.Models
{
    public record CategoryResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

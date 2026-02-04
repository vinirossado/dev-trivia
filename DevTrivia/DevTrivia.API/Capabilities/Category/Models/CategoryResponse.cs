namespace DevTrivia.API.Capabilities.Category.Models
{
    public record CategoryResponse
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

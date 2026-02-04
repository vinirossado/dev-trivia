namespace DevTrivia.API.Capabilities.Question.Models
{
    public record QuestionRequest
    {
        public required string Title { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public required string Difficulty { get; set; } = string.Empty;
    }
}

namespace DevTrivia.API.Capabilities.Question.Models
{
    public record QuestionResponse
    {
        public long Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
    }
}

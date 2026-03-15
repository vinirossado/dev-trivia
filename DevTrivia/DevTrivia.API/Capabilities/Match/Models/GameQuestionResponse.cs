namespace DevTrivia.API.Capabilities.Match.Models;

public record GameQuestionResponse
{
    public long QuestionId { get; init; }
    public string Title { get; init; } = null!;
    public int QuestionNumber { get; init; }
    public int TotalQuestions { get; init; }
    public IEnumerable<GameAnswerOption> Options { get; init; } = [];
}

public record GameAnswerOption
{
    public long Id { get; init; }
    public string Text { get; init; } = null!;
}
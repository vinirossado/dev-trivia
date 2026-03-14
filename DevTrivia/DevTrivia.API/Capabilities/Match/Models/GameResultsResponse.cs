namespace DevTrivia.API.Capabilities.Match.Models;

public record GameResultsResponse
{
    public long MatchId { get; init; }
    public int TotalQuestions { get; init; }
    public int CorrectAnswers { get; init; }
    public int Score { get; init; }
    public IEnumerable<QuestionResult> Questions { get; init; } = [];
}

public record QuestionResult
{
    public long QuestionId { get; init; }
    public string Title { get; init; } = null!;
    public long? SelectedAnswerOptionId { get; init; }
    public long CorrectAnswerOptionId { get; init; }
    public bool IsCorrect { get; init; }
}

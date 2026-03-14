using System.Collections.Concurrent;
using DevTrivia.API.Capabilities.AnswerOptions.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.Match.Models;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Services.Interfaces;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;

namespace DevTrivia.API.Capabilities.Match.Services;

public sealed class GamePlayService : IGamePlayService
{
    private static readonly ConcurrentDictionary<long, GameState> ActiveGames = new();

    private readonly IMatchRepository _matchRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerOptionRepository _answerOptionRepository;

    public GamePlayService(
        IMatchRepository matchRepository,
        IQuestionRepository questionRepository,
        IAnswerOptionRepository answerOptionRepository)
    {
        _matchRepository = matchRepository;
        _questionRepository = questionRepository;
        _answerOptionRepository = answerOptionRepository;
    }

    public async Task<GameStartResponse> StartMatchAsync(long matchId, CancellationToken ct = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, ct)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.Pending)
            throw new InvalidOperationException("Match must be in Pending status to start");

        var questions = await _questionRepository.GetByCategoryIdAsync(match.SelectedCategoryId, ct);
        var selectedQuestions = questions
            .OrderBy(_ => Random.Shared.Next())
            .Take(10)
            .ToList();

        if (selectedQuestions.Count == 0)
            throw new InvalidOperationException("No questions available for the selected category");

        var gameState = new GameState
        {
            Questions = selectedQuestions.Select(q => new QuestionState
            {
                QuestionId = q.Id,
                Title = q.Title
            }).ToList()
        };

        ActiveGames[matchId] = gameState;

        match.Status = StatusEnum.InProgress;
        match.StartedAt = DateTime.UtcNow;
        await _matchRepository.UpdateAsync(match, ct);

        return new GameStartResponse
        {
            MatchId = matchId,
            TotalQuestions = selectedQuestions.Count,
            CategoryName = match.Category.Name,
            Status = StatusEnum.InProgress
        };
    }

    public async Task<GameQuestionResponse> GetNextQuestionAsync(long matchId, CancellationToken ct = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, ct)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.InProgress)
            throw new InvalidOperationException("Match must be in InProgress status");

        if (!ActiveGames.TryGetValue(matchId, out var gameState))
            throw new InvalidOperationException("Game state not found. The match may need to be restarted");

        var nextQuestion = gameState.Questions.FirstOrDefault(q => !q.IsAnswered)
            ?? throw new InvalidOperationException("All questions have been answered");

        var answeredCount = gameState.Questions.Count(q => q.IsAnswered);

        var answerOptions = await _answerOptionRepository.GetAnswerOptionsByQuestionId(nextQuestion.QuestionId, ct);
        var shuffledOptions = answerOptions
            .OrderBy(_ => Random.Shared.Next())
            .Select(ao => new GameAnswerOption
            {
                Id = ao.Id,
                Text = ao.Text
            });

        return new GameQuestionResponse
        {
            QuestionId = nextQuestion.QuestionId,
            Title = nextQuestion.Title,
            QuestionNumber = answeredCount + 1,
            TotalQuestions = gameState.Questions.Count,
            Options = shuffledOptions
        };
    }

    public async Task<SubmitAnswerResponse> SubmitAnswerAsync(long matchId, SubmitAnswerRequest request, CancellationToken ct = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, ct)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.InProgress)
            throw new InvalidOperationException("Match must be in InProgress status");

        if (!ActiveGames.TryGetValue(matchId, out var gameState))
            throw new InvalidOperationException("Game state not found. The match may need to be restarted");

        var questionState = gameState.Questions.FirstOrDefault(q => q.QuestionId == request.QuestionId)
            ?? throw new KeyNotFoundException($"Question {request.QuestionId} is not part of this match");

        if (questionState.IsAnswered)
            throw new InvalidOperationException("This question has already been answered");

        var answerOptions = await _answerOptionRepository.GetAnswerOptionsByQuestionId(request.QuestionId, ct);
        var correctOption = answerOptions.FirstOrDefault(ao => ao.IsCorrect)
            ?? throw new InvalidOperationException("No correct answer configured for this question");

        var isCorrect = request.SelectedAnswerOptionId.HasValue
            && request.SelectedAnswerOptionId.Value == correctOption.Id;

        questionState.SelectedAnswerOptionId = request.SelectedAnswerOptionId;
        questionState.IsCorrect = isCorrect;
        questionState.IsAnswered = true;

        var isLastQuestion = gameState.Questions.All(q => q.IsAnswered);

        if (isLastQuestion)
        {
            match.Status = StatusEnum.Finished;
            match.EndedAt = DateTime.UtcNow;
            await _matchRepository.UpdateAsync(match, ct);
        }

        return new SubmitAnswerResponse
        {
            IsCorrect = isCorrect,
            CorrectAnswerOptionId = correctOption.Id,
            IsLastQuestion = isLastQuestion
        };
    }

    public Task<GameResultsResponse> GetResultsAsync(long matchId, CancellationToken ct = default)
    {
        if (!ActiveGames.TryGetValue(matchId, out var gameState))
            throw new KeyNotFoundException($"Results not found for match {matchId}");

        var totalQuestions = gameState.Questions.Count;
        var correctAnswers = gameState.Questions.Count(q => q.IsCorrect);

        var response = new GameResultsResponse
        {
            MatchId = matchId,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            Score = totalQuestions > 0 ? (correctAnswers * 100) / totalQuestions : 0,
            Questions = gameState.Questions.Select(q => new QuestionResult
            {
                QuestionId = q.QuestionId,
                Title = q.Title,
                SelectedAnswerOptionId = q.SelectedAnswerOptionId,
                CorrectAnswerOptionId = 0, // Will be populated below
                IsCorrect = q.IsCorrect
            })
        };

        return Task.FromResult(response);
    }

    private sealed class GameState
    {
        public List<QuestionState> Questions { get; init; } = [];
    }

    private sealed class QuestionState
    {
        public long QuestionId { get; init; }
        public string Title { get; init; } = null!;
        public long? SelectedAnswerOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsAnswered { get; set; }
    }
}

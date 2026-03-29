using Azure.Core;
using DevTrivia.API.Capabilities.AnswerOptions.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.Match.Models;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Services.Interfaces;
using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using DevTrivia.API.Capabilities.PlayerAnswer.Repositories.Interfaces;
using DevTrivia.API.Capabilities.PlayerStats.Enums;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;
using DevTrivia.API.Capabilities.Question.Repositories.Interfaces;

namespace DevTrivia.API.Capabilities.Match.Services;

public sealed class GamePlayService : IGamePlayService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerOptionRepository _answerOptionRepository;
    private readonly IPlayerAnswerRepository _playerAnswerRepository;
    private readonly IPlayerStatsService _playerStatsService;

    public GamePlayService(
        IMatchRepository matchRepository,
        IQuestionRepository questionRepository,
        IAnswerOptionRepository answerOptionRepository,
        IPlayerStatsService playerStatsService,
        IPlayerAnswerRepository playerAnswerRepository)
    {
        _matchRepository = matchRepository;
        _questionRepository = questionRepository;
        _answerOptionRepository = answerOptionRepository;
        _playerAnswerRepository = playerAnswerRepository;
        _playerStatsService = playerStatsService;
    }

    public async Task<GameStartResponse> StartMatchAsync(long matchId, CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, cancellationToken)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.Pending)
        {
            throw new InvalidOperationException("Match must be in Pending status to start");
        }

        var questions = (await _questionRepository.GetByCategoryIdAsync(match.SelectedCategoryId, cancellationToken)).ToList();

        if (questions.Count == 0)
        {
            throw new InvalidOperationException("No questions available for the selected category");
        }

        Random.Shared.Shuffle(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(questions));
        var selectedQuestions = questions.Take(10).ToList();

        foreach (var question in selectedQuestions)
        {
            await _playerAnswerRepository.AddAsync(new PlayerAnswerEntity
            {
                MatchId = matchId,
                QuestionId = question.Id,
                IsCorrect = false,
                AnsweredAt = default
            }, cancellationToken);
        }

        match.Status = StatusEnum.InProgress;
        match.CreatedAt = DateTime.UtcNow;
        await _matchRepository.UpdateAsync(match, cancellationToken);

        return new GameStartResponse
        {
            MatchId = matchId,
            TotalQuestions = selectedQuestions.Count,
            CategoryName = match.Category.Name,
            Status = StatusEnum.InProgress
        };
    }

    public async Task<GameQuestionResponse> GetNextQuestionAsync(long matchId, CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, cancellationToken)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.InProgress)
        {
            throw new InvalidOperationException("Match must be in InProgress status");
        }

        var nextPlayerAnswer = await _playerAnswerRepository.GetUnansweredByMatchIdAsync(matchId, cancellationToken)
            ?? throw new InvalidOperationException("All questions have been answered");

        var allPlayerAnswers = await _playerAnswerRepository.GetByMatchIdAsync(matchId, cancellationToken);
        var playerAnswerList = allPlayerAnswers.ToList();
        var answeredCount = playerAnswerList.Count(pa => pa.AnsweredAt != default);

        var answerOptions = (await _answerOptionRepository.GetAnswerOptionsByQuestionId(nextPlayerAnswer.QuestionId, cancellationToken)).ToList();
        Random.Shared.Shuffle(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(answerOptions));

        var shuffledOptions = answerOptions.Select(ao => new GameAnswerOption
        {
            Id = ao.Id,
            Text = ao.Text
        });

        return new GameQuestionResponse
        {
            QuestionId = nextPlayerAnswer.QuestionId,
            Title = nextPlayerAnswer.Question.Title,
            QuestionNumber = answeredCount + 1,
            TotalQuestions = playerAnswerList.Count,
            Options = shuffledOptions
        };
    }

    public async Task<SubmitAnswerResponse> SubmitAnswerAsync(long matchId, SubmitAnswerRequest request, CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, cancellationToken)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.InProgress)
        {
            throw new InvalidOperationException("Match must be in InProgress status");
        }

        var playerAnswer = await _playerAnswerRepository.GetByMatchAndQuestionAsync(matchId, request.QuestionId, cancellationToken)
            ?? throw new KeyNotFoundException($"Question {request.QuestionId} is not part of this match");

        if (playerAnswer.AnsweredAt != default)
        {
            throw new InvalidOperationException("This question has already been answered");
        }

        var answerOptions = await _answerOptionRepository.GetAnswerOptionsByQuestionId(request.QuestionId, cancellationToken);
        var answerOptionList = answerOptions.ToList();
        var correctOption = answerOptionList.FirstOrDefault(ao => ao.IsCorrect)
            ?? throw new InvalidOperationException("No correct answer configured for this question");

        if (request.SelectedAnswerOptionId.HasValue
            && answerOptionList.All(ao => ao.Id != request.SelectedAnswerOptionId.Value))
        {
            throw new KeyNotFoundException(
                $"Answer option {request.SelectedAnswerOptionId} does not belong to question {request.QuestionId}");
        }

        var isCorrect = request.SelectedAnswerOptionId.HasValue
            && request.SelectedAnswerOptionId.Value == correctOption.Id;

        playerAnswer.SelectedAnswerOptionId = request.SelectedAnswerOptionId;
        playerAnswer.IsCorrect = isCorrect;
        playerAnswer.AnsweredAt = DateTime.UtcNow;
        await _playerAnswerRepository.UpdateAsync(playerAnswer, cancellationToken);

        var unanswered = await _playerAnswerRepository.GetUnansweredByMatchIdAsync(matchId, cancellationToken);
        var isLastQuestion = unanswered is null;

        if (isLastQuestion)
        {
            match.Status = StatusEnum.Finished;
            match.CreatedAt = DateTime.UtcNow;
            await _matchRepository.UpdateAsync(match, cancellationToken);
        }

        return new SubmitAnswerResponse
        {
            IsCorrect = isCorrect,
            CorrectAnswerOptionId = correctOption.Id,
            IsLastQuestion = isLastQuestion
        };
    }

    public async Task<GameResultsResponse> GetResultsAsync(long matchId, CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(matchId, cancellationToken)
            ?? throw new KeyNotFoundException($"Match with ID {matchId} not found");

        if (match.Status != StatusEnum.Finished)
        {
            throw new InvalidOperationException("Match must be in Finished status to view results");
        }

        var playerAnswers = (await _playerAnswerRepository.GetByMatchIdAsync(matchId, cancellationToken)).ToList();
        var totalQuestions = playerAnswers.Count;
        var correctAnswers = playerAnswers.Count(pa => pa.IsCorrect);

        var correctOptionsByQuestion = new Dictionary<long, long>();
        foreach (var pa in playerAnswers)
        {
            var options = await _answerOptionRepository.GetAnswerOptionsByQuestionId(pa.QuestionId, cancellationToken);
            var correct = options.FirstOrDefault(ao => ao.IsCorrect);
            if (correct is not null)
            {
                correctOptionsByQuestion[pa.QuestionId] = correct.Id;
            }
        }

        var playerStats = await _playerStatsService.GetStatsByUserIdAsync(match.UserId);
        if (playerStats == null)
        {
            await _playerStatsService.CreateAsync(new PlayerStatsRequest
            {
                UserId = match.UserId,
                TotalCorrect = correctAnswers,
                EloRating = EloRating.Bronze
            }, cancellationToken);
        }
        else
        {
            await _playerStatsService.UpdateAsync(new PlayerStatsRequest
            {
                UserId = match.UserId,
                TotalCorrect =+ correctAnswers,
                EloRating = EloRating.Prata
            }, cancellationToken);
        }

        return new GameResultsResponse
        {
            MatchId = matchId,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            Score = totalQuestions > 0 ? (correctAnswers * 100) / totalQuestions : 0,
            Questions = playerAnswers.Select(pa => new QuestionResult
            {
                QuestionId = pa.QuestionId,
                Title = pa.Question.Title,
                SelectedAnswerOptionId = pa.SelectedAnswerOptionId,
                CorrectAnswerOptionId = correctOptionsByQuestion.GetValueOrDefault(pa.QuestionId),
                IsCorrect = pa.IsCorrect
            })
        };
    }
}
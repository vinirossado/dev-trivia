using DevTrivia.API.Capabilities.Match.Models;

namespace DevTrivia.API.Capabilities.Match.Services.Interfaces;

public interface IGamePlayService
{
    Task<GameStartResponse> StartMatchAsync(long matchId, CancellationToken cancellationToken = default);
    Task<GameQuestionResponse> GetNextQuestionAsync(long matchId, CancellationToken cancellationToken = default);
    Task<SubmitAnswerResponse> SubmitAnswerAsync(long matchId, SubmitAnswerRequest request, CancellationToken cancellationToken = default);
    Task<GameResultsResponse> GetResultsAsync(long matchId, CancellationToken cancellationToken = default);
}
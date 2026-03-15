using DevTrivia.API.Capabilities.Match.Models;

namespace DevTrivia.API.Capabilities.Match.Services.Interfaces;

public interface IGamePlayService
{
    Task<GameStartResponse> StartMatchAsync(long matchId, CancellationToken ct = default);
    Task<GameQuestionResponse> GetNextQuestionAsync(long matchId, CancellationToken ct = default);
    Task<SubmitAnswerResponse> SubmitAnswerAsync(long matchId, SubmitAnswerRequest request, CancellationToken ct = default);
    Task<GameResultsResponse> GetResultsAsync(long matchId, CancellationToken ct = default);
}
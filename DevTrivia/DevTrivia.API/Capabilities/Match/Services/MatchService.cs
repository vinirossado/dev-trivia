using Azure.Core;
using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Models;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Services.Interfaces;

namespace DevTrivia.API.Capabilities.Match.Services;

public sealed class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly ILogger<MatchService> _logger;

    public MatchService(
        IMatchRepository matchRepository,
        ILogger<MatchService> logger)
    {
        _matchRepository = matchRepository;
        _logger = logger;
    }

    public async Task<MatchEntity> CreateAsync(MatchEntity match, CancellationToken cancellationToken = default)
    {

        if ((int)match.Status <= 0 || (int)match.Status > 3)
        {
            throw new InvalidOperationException($"Mitch with status {match.Status} cannot exist");
        }

        return await _matchRepository.AddAsync(match, cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var categoryExists = await _matchRepository.ExistsAsync(id, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Match with id {id} not found");
        }

        return await _matchRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<MatchEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _matchRepository.GetAllAsync(cancellationToken);
    }

    public async Task<MatchEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var category = await _matchRepository.GetByIdAsync(id, cancellationToken);
        if (category == null)
        {
            throw new KeyNotFoundException($"Match with id {id} not found");
        }

        return category;
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _matchRepository.GetTotalCountAsync(cancellationToken);
    }

    public async Task<MatchEntity> UpdateAsync(MatchRequest request, long id, CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(id, cancellationToken);
        if (match == null)
        {
            throw new KeyNotFoundException($"Match with id {id} not found");
        }
        else if ((int)request.Status <= 0 || (int)request.Status > 3)
        {
            throw new KeyNotFoundException($"Match with status {request.Status} cannot exist");
        }

        match.Status = request.Status;
        match.SelectedCategoryId = request.SelectedCategoryId;

        return await _matchRepository.UpdateAsync(match, cancellationToken);
    }
}
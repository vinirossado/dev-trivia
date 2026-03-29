using DevTrivia.API.Capabilities.Category.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Extensions;
using DevTrivia.API.Capabilities.Match.Models;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Match.Services.Interfaces;
using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;

namespace DevTrivia.API.Capabilities.Match.Services;

public sealed class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPlayerStatsRepository _playerStatsRepository;
    private readonly ILogger<MatchService> _logger;

    public MatchService(
        IMatchRepository matchRepository,
        ICategoryRepository categoryRepository,
        IPlayerStatsRepository playerStatsRepository,
        ILogger<MatchService> logger)
    {
        _matchRepository = matchRepository;
        _categoryRepository = categoryRepository;
        _playerStatsRepository = playerStatsRepository;
        _logger = logger;
    }

    public async Task<MatchEntity> CreateAsync(MatchRequest request, CancellationToken cancellationToken = default)
    {
        if (!request.Status.IsValid())
        {
            throw new InvalidOperationException($"Match with status {request.Status} is not valid");
        }

        var categoryExists = await _categoryRepository.ExistsAsync(request.SelectedCategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category with id {request.SelectedCategoryId} not found");
        }

        return await _matchRepository.AddAsync(request.ToEntity(), cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var exists = await _matchRepository.ExistsAsync(id, cancellationToken);
        if (!exists)
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
        var match = await _matchRepository.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Match with id {id} not found");

        return match;
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _matchRepository.GetTotalCountAsync(cancellationToken);
    }

    public async Task<MatchEntity> UpdateAsync(MatchRequest request, long id,
        CancellationToken cancellationToken = default)
    {
        var match = await _matchRepository.GetByIdAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Match with id {id} not found");

        if (!request.Status.IsValid())
        {
            throw new InvalidOperationException($"Match with status {request.Status} is not valid");
        }

        match.Status = request.Status;
        match.SelectedCategoryId = request.SelectedCategoryId;

        return await _matchRepository.UpdateAsync(match, cancellationToken);
    }
}
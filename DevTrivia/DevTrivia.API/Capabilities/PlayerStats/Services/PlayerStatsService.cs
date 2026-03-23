using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Extensions;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;
using DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;

namespace DevTrivia.API.Capabilities.PlayerStats.Services;

public sealed class PlayerStatsService : IPlayerStatsService
{
    private readonly IPlayerStatsRepository _playerStatsRepository;
    private readonly ILogger<PlayerStatsService> _logger;

    public PlayerStatsService(
        IPlayerStatsRepository playerStatsRepository,
        ILogger<PlayerStatsService> logger)
    {
        _playerStatsRepository = playerStatsRepository;
        _logger = logger;
    }

    public async Task<PlayerStatsEntity> CreateAsync(PlayerStatsRequest request, CancellationToken cancellationToken = default)
    {
        var playerStatsExists = await _playerStatsRepository.GetStatsByUserIdAsync(request.UserId);
        if (playerStatsExists is not null)
        {
            throw new InvalidOperationException($"Player stats already exists for user {request.UserId}");
        }
        else if ((int)request.EloRating <= 0 || (int)request.EloRating >= 9)
        {
            throw new InvalidOperationException($"Player stats with EloRating {request.EloRating} cannot exist");
        }

        return await _playerStatsRepository.AddAsync(request.ToEntity(), cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var playerStats = await _playerStatsRepository.GetStatsByUserIdAsync(id, cancellationToken);
        if (playerStats == null)
        {
            throw new KeyNotFoundException($"Player stats with id {id} not found");
        }

        return await _playerStatsRepository.DeleteAsync(playerStats.Id, cancellationToken);
    }

    public async Task<IEnumerable<PlayerStatsEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _playerStatsRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PlayerStatsEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var playerStats = await _playerStatsRepository.GetStatsByUserIdAsync(id, cancellationToken);
        if (playerStats == null)
        {
            throw new KeyNotFoundException($"Stats with id {id} not found");
        }

        return playerStats;
    }

    public async Task<PlayerStatsEntity?> GetStatsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var playerStats = await _playerStatsRepository.GetStatsByUserIdAsync(userId, cancellationToken);

        return playerStats;
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _playerStatsRepository.GetTotalCountAsync(cancellationToken);
    }

    public async Task<PlayerStatsEntity> UpdateAsync(PlayerStatsRequest request, CancellationToken cancellationToken = default)
    {
        var playerStats = await _playerStatsRepository.GetStatsByUserIdAsync(request.UserId, cancellationToken);
        if (playerStats == null)
        {
            throw new KeyNotFoundException($"Stats with id {request.UserId} not found");
        }
        else if ((int)request.EloRating <= 0 || (int)request.EloRating >= 9)
        {
            throw new InvalidOperationException($"Stats with status {request.EloRating} cannot exist");
        }

        playerStats.TotalCorrect += request.TotalCorrect;
        playerStats.TotalMatches++;
        playerStats.EloRating = request.EloRating;

        return await _playerStatsRepository.UpdateAsync(playerStats, cancellationToken);
    }
}
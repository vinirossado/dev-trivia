using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using DevTrivia.API.Capabilities.PlayerStats.Models;
using DevTrivia.API.Capabilities.PlayerStats.Repositories.Interfaces;
using DevTrivia.API.Capabilities.PlayerStats.Services.Interfaces;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;

namespace DevTrivia.API.Capabilities.PlayerStats.Services;

public sealed class PlayerStatsService : IPlayerStatsService
{
    private readonly IPlayerStatsRepository _playerstatsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<PlayerStatsService> _logger;

    public PlayerStatsService(
        IPlayerStatsRepository playerstatsRepository,
        IUserRepository userRepository,
        ILogger<PlayerStatsService> logger)
    {
        _playerstatsRepository = playerstatsRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<PlayerStatsEntity> CreateAsync(PlayerStatsEntity playerstats, CancellationToken cancellationToken = default)
    {
        var playerstatsExists = await _playerstatsRepository.ExistsByUserIdAsync(playerstats.UserId);
        if (playerstatsExists)
        {
            throw new InvalidOperationException($"Player stats already exists for user {playerstats.UserId}");
        }
        else if ((int)playerstats.EloRating <= 0 || (int)playerstats.EloRating >= 9)
        {
            throw new InvalidOperationException($"Player stats with EloRating {playerstats.EloRating} cannot exist");
        }

        return await _playerstatsRepository.AddAsync(playerstats, cancellationToken);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.ExistsAsync(id, cancellationToken);
        if (!user)
        {
            throw new KeyNotFoundException($"Stats with id {id} not found");
        }

        var UserId = await _playerstatsRepository.StatsAsync(id, cancellationToken);
        var deletedId = UserId.Id;
        return await _playerstatsRepository.DeleteAsync(deletedId, cancellationToken);
    }

    public async Task<IEnumerable<PlayerStatsEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _playerstatsRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PlayerStatsEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {

        var userExist = await _userRepository.ExistsAsync(id, cancellationToken);
        if (!userExist)
        {
            throw new InvalidOperationException($"Player stats already exists for user");
        }

        var playerstats = await _playerstatsRepository.StatsAsync(id, cancellationToken);
        if(playerstats == null)
        {
            throw new KeyNotFoundException($"Stats with id {id} not found");
        }

        return playerstats;
    }

    public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _playerstatsRepository.GetTotalCountAsync(cancellationToken);
    }

    public async Task<PlayerStatsEntity> UpdateAsync(PlayerStatsRequest request, long id, CancellationToken cancellationToken = default)
    {
        var palyerstats = await _playerstatsRepository.StatsAsync(id, cancellationToken);
        if (palyerstats == null)
        {
            throw new KeyNotFoundException($"Stats with id {id} not found");
        }
        else if ((int)request.EloRating <= 0 || (int)request.EloRating > 9)
        {
            throw new KeyNotFoundException($"Stats with status {request.EloRating} cannot exist");
        }

        palyerstats.TotalMatches = request.TotalMatches;
        palyerstats.TotalCorrect = request.TotalCorrect;
        palyerstats.EloRating = request.EloRating;

        return await _playerstatsRepository.UpdateAsync(palyerstats, cancellationToken);
    }
}
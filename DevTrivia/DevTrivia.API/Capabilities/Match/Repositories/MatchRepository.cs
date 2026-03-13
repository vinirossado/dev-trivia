using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Infrastructure.Logging;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.Match.Repositories;

public sealed class MatchRepository : BaseRepository<MatchEntity>, IMatchRepository
{
    private readonly ILogger<MatchRepository> _logger;

    public MatchRepository(TriviaDbContext context, ILogger<MatchRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<bool> NameExistsAsync(int Id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbSet
                .AsNoTracking()
                .AnyAsync(c => c.Id == Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.DatabaseError("checking if category name exists", ex.Message, ex);
            throw;
        }
    }
}
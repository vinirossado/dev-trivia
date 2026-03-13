using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Repositories.Interfaces;
using DevTrivia.API.Capabilities.Shared.Repositories;
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

    public override async Task<MatchEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<MatchEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.Category)
            .ToListAsync(cancellationToken);
    }
}

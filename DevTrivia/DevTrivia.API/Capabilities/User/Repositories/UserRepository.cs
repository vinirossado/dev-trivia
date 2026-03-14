using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Capabilities.User.Database.Entities;
using DevTrivia.API.Capabilities.User.Repositories.Interfaces;
using DevTrivia.API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Capabilities.User.Repositories;

public sealed class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(TriviaDbContext context) : base(context)
    {
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }
}

using DevTrivia.API.Capabilities.Shared.Repositories;
using DevTrivia.API.Capabilities.User.Database.Entities;

namespace DevTrivia.API.Capabilities.User.Repositories.Interfaces;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
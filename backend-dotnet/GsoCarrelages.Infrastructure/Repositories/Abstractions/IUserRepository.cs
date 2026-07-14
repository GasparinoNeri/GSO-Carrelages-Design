using GsoCarrelages.Infrastructure.Models;

namespace GsoCarrelages.Infrastructure.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByEmailAndPasswordAsync(
        string email,
        string password
    );
}

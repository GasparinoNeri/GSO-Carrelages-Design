using GsoCarrelages.Infrastructure.Models;

namespace GsoCarrelages.Infrastructure.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(long id);

    Task<long> CreateAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task<bool> EmailExistsAsync(string email);
}

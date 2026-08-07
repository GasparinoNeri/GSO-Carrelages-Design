using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.IGateways;

public interface IUserGateway
{
    Task<User?> GetByEmailAsync(string email);

    Task<long> CreateAsync(User user);

    Task<bool> EmailExistsAsync(string email);

    Task<User?> GetByIdAsync(long id);

    Task<bool> UpdateAsync(User user);
}

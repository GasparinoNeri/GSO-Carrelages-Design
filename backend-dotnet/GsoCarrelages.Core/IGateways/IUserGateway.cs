using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.IGateways;

public interface IUserGateway
{
    Task<User?> GetByEmailAndPasswordAsync(string email, string password);
}

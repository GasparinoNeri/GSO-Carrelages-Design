using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAndPasswordAsync(string email, string password);
}
using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.UseCases.Abstractions;

public interface IAuthUseCases
{
    Task<User?> LoginAsync(string email, string password);
}

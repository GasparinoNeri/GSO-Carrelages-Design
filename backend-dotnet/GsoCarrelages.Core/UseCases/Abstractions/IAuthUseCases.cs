using GsoCarrelages.Core.Entities;

namespace GsoCarrelages.Core.UseCases.Abstractions;

public interface IAuthUseCases
{
    Task<User?> LoginAsync(string email, string password);
    
    Task<long> RegisterAsync(User user);

    Task<User?> GetProfileAsync(long id);

    Task<bool> UpdateProfileAsync(User user);  
}

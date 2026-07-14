using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Core.UseCases.Abstractions;

namespace GsoCarrelages.Core.UseCases;

public class AuthUseCases : IAuthUseCases
{
    private readonly IUserGateway _userGateway;

    public AuthUseCases(IUserGateway userGateway)
    {
        _userGateway = userGateway
            ?? throw new ArgumentNullException(nameof(userGateway));
    }

    public Task<User?> LoginAsync(string email, string password)
    {
        return _userGateway.GetByEmailAndPasswordAsync(email, password);
    }
}

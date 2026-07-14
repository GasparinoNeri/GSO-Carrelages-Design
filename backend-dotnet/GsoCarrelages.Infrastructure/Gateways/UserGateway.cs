using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Infrastructure.Repositories.Abstractions;
using InfrastructureUser = GsoCarrelages.Infrastructure.Models.User;

namespace GsoCarrelages.Infrastructure.Gateways;

public class UserGateway : IUserGateway
{
    private readonly IUserRepository _userRepository;

    public UserGateway(IUserRepository userRepository)
    {
        _userRepository = userRepository
            ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User?> GetByEmailAndPasswordAsync(
        string email,
        string password
    )
    {
        var infrastructureUser =
            await _userRepository.GetByEmailAndPasswordAsync(email, password);

        return infrastructureUser is null
            ? null
            : ToCoreUser(infrastructureUser);
    }

    private static User ToCoreUser(InfrastructureUser user)
    {
        return new User
        {
            IdUtilisateur = user.IdUtilisateur,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            MotDePasse = user.MotDePasse,
            Role = user.Role,
            Actif = user.Actif
        };
    }
}

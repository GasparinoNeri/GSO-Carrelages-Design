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

    public async Task<User?> GetByEmailAsync(string email)
    {
        var infrastructureUser =
            await _userRepository.GetByEmailAsync(email);

        return infrastructureUser is null
            ? null
            : ToCoreUser(infrastructureUser);
    }

    public Task<long> CreateAsync(User user)
    {
        var infrastructureUser = new InfrastructureUser
        {
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            Telephone = user.Telephone,
            Adresse = user.Adresse,
            DateNaissance = user.DateNaissance,
            PhotoProfil = user.PhotoProfil,
            MotDePasse = user.MotDePasse,
            Role = user.Role,
            Actif = user.Actif
        };

        return _userRepository.CreateAsync(infrastructureUser);
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        return _userRepository.EmailExistsAsync(email);
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        var infrastructureUser =
            await _userRepository.GetByIdAsync(id);

        return infrastructureUser is null
            ? null
            : ToCoreUser(infrastructureUser);
    }

    public Task<bool> UpdateAsync(User user)
    {
        var infrastructureUser = new InfrastructureUser
        {
            IdUtilisateur = user.IdUtilisateur,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            Telephone = user.Telephone,
            Adresse = user.Adresse,
            DateNaissance = user.DateNaissance,
            PhotoProfil = user.PhotoProfil,
            MotDePasse = user.MotDePasse,
            Role = user.Role,
            Actif = user.Actif,
            CreatedAt = user.CreatedAt
        };

        return _userRepository.UpdateAsync(infrastructureUser);
    }

    private static User ToCoreUser(InfrastructureUser user)
    {
        return new User
        {
            IdUtilisateur = user.IdUtilisateur,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            Telephone = user.Telephone,
            Adresse = user.Adresse,
            DateNaissance = user.DateNaissance,
            PhotoProfil = user.PhotoProfil,
            MotDePasse = user.MotDePasse,
            Role = user.Role,
            Actif = user.Actif,
            CreatedAt = user.CreatedAt
        };
    }
}

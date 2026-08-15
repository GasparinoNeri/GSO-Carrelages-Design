using BCrypt.Net;
using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.IGateways;
using GsoCarrelages.Core.UseCases.Abstractions;

namespace GsoCarrelages.Core.UseCases;

public class AuthUseCases : IAuthUseCases
{
    private readonly IUserGateway _userGateway;
    private readonly IClientGateway _clientGateway;

    public AuthUseCases(
        IUserGateway userGateway,
        IClientGateway clientGateway
    )
    {
        _userGateway = userGateway
            ?? throw new ArgumentNullException(nameof(userGateway));

        _clientGateway = clientGateway
            ?? throw new ArgumentNullException(nameof(clientGateway));
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userGateway.GetByEmailAsync(email);

        if (user is null)
        {
            return null;
        }

        var passwordIsValid = BCrypt.Net.BCrypt.Verify(
            password,
            user.MotDePasse
        );

        if (!passwordIsValid)
        {
            return null;
        }

        return user;
    }

    public async Task<long> RegisterAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Nom))
        {
            throw new ArgumentException("Le nom est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("L'email est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(user.MotDePasse) ||
            user.MotDePasse.Length < 8)
        {
            throw new ArgumentException(
                "Le mot de passe doit contenir au moins 8 caractères."
            );
        }

        if (await _userGateway.EmailExistsAsync(user.Email))
        {
            throw new InvalidOperationException(
                "Cette adresse e-mail est déjà utilisée."
            );
        }

        user.MotDePasse = BCrypt.Net.BCrypt.HashPassword(
            user.MotDePasse
        );

        user.Role = "client";
        user.Actif = true;

        var userId = await _userGateway.CreateAsync(user);

        var existingClient = await _clientGateway.GetByEmailAsync(user.Email);

        if (existingClient is null)
        {
            var client = new Client
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Tel = user.Telephone,
                Statut = "actif"
            };

            await _clientGateway.CreateAsync(client);
        }

        return userId;
    }

    public Task<User?> GetProfileAsync(long id)
    {
        return _userGateway.GetByIdAsync(id);
    }

    public async Task<bool> UpdateProfileAsync(User user)
    {
        var existingUser = await _userGateway.GetByIdAsync(user.IdUtilisateur);

        if (existingUser is null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(user.Nom))
        {
            throw new ArgumentException("Le nom est obligatoire.");
        }

        existingUser.Nom = user.Nom;
        existingUser.Prenom = user.Prenom;
        existingUser.Telephone = user.Telephone;
        existingUser.Adresse = user.Adresse;
        existingUser.DateNaissance = user.DateNaissance;
        existingUser.PhotoProfil = user.PhotoProfil;

        return await _userGateway.UpdateAsync(existingUser);
    }
}

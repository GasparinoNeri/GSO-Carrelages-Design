using GsoCarrelages.Core.Entities;
using GsoCarrelages.Core.UseCases.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GsoCarrelages.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCases _authUseCases;

    public AuthController(IAuthUseCases authUseCases)
    {
        _authUseCases = authUseCases;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _authUseCases.LoginAsync(
            request.Email,
            request.Password
        );

        if (user is null)
        {
            return Unauthorized("Email ou mot de passe incorrect.");
        }

                return Ok(new
        {
            user.IdUtilisateur,
            user.Nom,
            user.Prenom,
            user.Email,
            user.Telephone,
            user.Adresse,
            user.DateNaissance,
            user.PhotoProfil,
            user.Role,
            user.Actif,
            user.CreatedAt
        });
    }

        [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var user = new User
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                Telephone = request.Telephone,
                Adresse = request.Adresse,
                DateNaissance = request.DateNaissance,
                PhotoProfil = request.PhotoProfil,
                MotDePasse = request.Password
            };

            var newId = await _authUseCases.RegisterAsync(user);

            user.IdUtilisateur = newId;

                        return Ok(new
            {
                user.IdUtilisateur,
                user.Nom,
                user.Prenom,
                user.Email,
                user.Telephone,
                user.Adresse,
                user.DateNaissance,
                user.PhotoProfil,
                user.Role,
                user.Actif
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfile(long id)
    {
        var user = await _authUseCases.GetProfileAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            user.IdUtilisateur,
            user.Nom,
            user.Prenom,
            user.Email,
            user.Telephone,
            user.Adresse,
            user.DateNaissance,
            user.PhotoProfil,
            user.Role,
            user.Actif,
            user.CreatedAt
        });
    }

    [HttpPut("profile/{id}")]
    public async Task<IActionResult> UpdateProfile(
        long id,
        UpdateProfileRequest request
    )
    {
        try
        {
            var user = new User
            {
                IdUtilisateur = id,
                Nom = request.Nom,
                Prenom = request.Prenom,
                Telephone = request.Telephone,
                Adresse = request.Adresse,
                DateNaissance = request.DateNaissance,
                PhotoProfil = request.PhotoProfil
            };

            var updated = await _authUseCases.UpdateProfileAsync(user);

            if (!updated)
            {
                return NotFound();
            }

            var updatedUser = await _authUseCases.GetProfileAsync(id);

            return Ok(new
            {
                updatedUser!.IdUtilisateur,
                updatedUser.Nom,
                updatedUser.Prenom,
                updatedUser.Email,
                updatedUser.Telephone,
                updatedUser.Adresse,
                updatedUser.DateNaissance,
                updatedUser.PhotoProfil,
                updatedUser.Role,
                updatedUser.Actif,
                updatedUser.CreatedAt
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public record LoginRequest(
    string Email,
    string Password
);

public record RegisterRequest(
    string Nom,
    string? Prenom,
    string Email,
    string? Telephone,
    string? Adresse,
    DateTime? DateNaissance,
    string? PhotoProfil,
    string Password
);

public record UpdateProfileRequest(
    string Nom,
    string? Prenom,
    string? Telephone,
    string? Adresse,
    DateTime? DateNaissance,
    string? PhotoProfil
);
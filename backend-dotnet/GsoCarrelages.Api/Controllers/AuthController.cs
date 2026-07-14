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

        return Ok(user);
    }
}

public record LoginRequest(
    string Email,
    string Password
);
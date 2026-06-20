using GsoCarrelages.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GsoCarrelages.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAndPasswordAsync(
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
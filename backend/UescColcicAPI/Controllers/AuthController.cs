using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Core;
using UescColcicAPI.Services.Auth;
namespace UescColcicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] User userLogin)
    {
        // Valide o usuário (exemplo simplificado)
        if (userLogin.Username == "string" && userLogin.Password == "string")
        {
            var token = _authService.GenerateJwtToken();
            return Ok(new { token });
        }

        return Unauthorized();
    }

    }
}

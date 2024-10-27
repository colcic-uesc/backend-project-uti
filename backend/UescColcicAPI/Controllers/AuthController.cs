using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Core;
using UescColcicAPI.Services.Auth;
using UescColcicAPI.Services.InputModels;
using UescColcicAPI.Services.ViewModel;
using UescColcicAPI.Services.ViewModels;

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
        public IActionResult Login([FromBody] LoginInputModel userLogin)
        {
            var user = _authService.ValidateUser(userLogin.Username, userLogin.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}

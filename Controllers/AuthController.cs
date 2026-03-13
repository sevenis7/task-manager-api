using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Models.Auth;
using TaskManager.Services.Auth;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var auth = await _authService.Login(model);

            return Ok(auth);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            var stringUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (stringUserId is null)
                return Unauthorized();

            int userId = Convert.ToInt32(stringUserId);
            
            await _authService.Logout(userId);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var auth = await _authService.Register(model);

            return Ok(auth);
        }

        
    }
}

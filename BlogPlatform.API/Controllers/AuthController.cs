using BlogPlatform.DTO.Auth;
using BlogPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokens(RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return Ok(result);
        }
    }
}

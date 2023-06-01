using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Backend.Application.Common.Models.Auth;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Core.Interfaces;

namespace Social_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resToken = await _authService.Authenticate(request);
            return Ok(CustomAPIResponse<AuthResponse>.Success(resToken, StatusCodes.Status200OK));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _authService.Register(request);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }
    }
}
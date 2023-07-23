using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Backend.Application.Common.Models.Auth;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Core.Interfaces.Auth;
using Social_Backend.Core.Interfaces.User;

namespace Social_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly ICurrentUserService _currentUserService;

        public AuthsController(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
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

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resToken = await _authService.RefreshToken(request);
            return Ok(CustomAPIResponse<AuthResponse>.Success(resToken, StatusCodes.Status200OK));
        }

        [HttpPost("revoke-token/{userId}")]
        public async Task<IActionResult> RevokeToken()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _authService.RevokeToken(_currentUserService.UserId);
            return Ok(CustomAPIResponse<string>.Success("Revoke token for this user successfully", StatusCodes.Status200OK));
        }

        [HttpPost("revoke-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RevokeAllToken()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _authService.RevokeAllToken();
            return Ok(CustomAPIResponse<string>.Success("Revoke token for all user successfully", StatusCodes.Status200OK));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Interfaces.User;
using System.Security.Claims;

namespace Social_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var user = await _userRepository.GetById(userId);
            return Ok(CustomAPIResponse<UserDTO>.Success(user, StatusCodes.Status200OK));
        }
    }
}
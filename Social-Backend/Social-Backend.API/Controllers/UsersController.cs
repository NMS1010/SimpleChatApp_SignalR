using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Common.Models.User;
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
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var user = await _userService.GetById(userId);
            return Ok(CustomAPIResponse<UserDTO>.Success(user, StatusCodes.Status200OK));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> FindUsersByName(UserSearchRequest request)
        {
            var users = await _userService.FindUsersByName(request);
            return Ok(CustomAPIResponse<PaginatedResult<UserDTO>>.Success(users, StatusCodes.Status200OK));
        }
    }
}
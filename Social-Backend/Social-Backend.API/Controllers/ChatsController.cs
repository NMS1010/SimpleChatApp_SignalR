using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Backend.Application.Common.Models.Chat;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.UserChat;

namespace Social_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatServices;
        private readonly IUserChatService _userChatServices;

        public ChatsController(IChatService chatServices, IUserChatService userChatServices)
        {
            _chatServices = chatServices;
            _userChatServices = userChatServices;
        }

        [HttpGet("get-chats")]
        public async Task<IActionResult> GetChats([FromForm] ChatGetPagingRequest request)
        {
            var chats = await _chatServices.GetChatsByUser(request);

            return Ok(CustomAPIResponse<PaginatedResult<ChatDTO>>.Success(chats, StatusCodes.Status200OK));
        }

        [HttpGet("get-chat")]
        public async Task<IActionResult> GetChatById([FromForm] int chatId)
        {
            var chat = await _chatServices.GetChatById(chatId);

            return Ok(CustomAPIResponse<ChatDTO>.Success(chat, StatusCodes.Status200OK));
        }

        [HttpPost("create/group")]
        public async Task<IActionResult> CreateGroupChat([FromForm] ChatCreateRequest request)
        {
            await _chatServices.CreateRoom(request);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPost("create/private")]
        public async Task<IActionResult> CreatePrivateChat([FromForm] ChatCreateRequest request)
        {
            await _chatServices.CreatePrivateRoom(request);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinChat([FromForm] string userId, [FromForm] int chatId)
        {
            await _userChatServices.JoinRoom(userId, chatId);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }
    }
}
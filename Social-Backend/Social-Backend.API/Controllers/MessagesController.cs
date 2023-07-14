using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Social_Backend.API.Hubs;
using Social_Backend.Application.Common.Models.CustomResponse;
using Social_Backend.Application.Common.Models.Message;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Interfaces.Message;

namespace Social_Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMessageByChat([FromQuery] MessageGetPagingRequest request)
        {
            var res = await _messageService.GetMessage(request);
            return Ok(CustomAPIResponse<PaginatedResult<MessageDTO>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage([FromForm] MessageCreateRequest request)
        {
            var message = await _messageService.CreateMessage(request);
            await _hubContext.Clients.Group(request.RoomId).SendAsync("ReceiveMessage", new
            {
                Text = message.Text,
                CreateDate = message.CreateDate
            });
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }
    }
}
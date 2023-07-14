using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Social_Backend.Application.Common.Models.Message;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Core.Interfaces.UserChat;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Social_Backend.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IUserChatService _userChatServices;

        public ChatHub(IMessageService messageService, IUserChatService userChatServices)
        {
            _messageService = messageService;
            _userChatServices = userChatServices;
        }

        public async Task JoinRoom(string roomId)
        {
            //await _userChatServices.JoinRoom(userId, chatId);
            //await Clients.Group(roomId).SendAsync("TipsMessage", $"{Context.User.Identity.Name} has joined this chat");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveRoom(string roomId, string userId, int chatId)
        {
            //await _userChatServices.LeaveRoom(userId, chatId);
            //await Clients.Group(roomId).SendAsync("TipsMessage", $"{Context.UserIdentifier} has leave this chat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendMessage(MessageCreateRequest request)
        {
            var message = await _messageService.CreateMessage(request);
            await Clients.Group(request.RoomId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
        }
    }
}
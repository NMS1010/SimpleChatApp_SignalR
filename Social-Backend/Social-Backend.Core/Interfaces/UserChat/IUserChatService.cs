using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.UserChat
{
    public interface IUserChatService
    {
        Task JoinRoom(string userId, int chatId);

        Task LeaveRoom(string userId, int chatId);
    }
}
using Microsoft.EntityFrameworkCore;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Core.Interfaces.UserChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IChatRepository ChatRepository { get; set; }
        IChatRoleRepository ChatRoleRepository { get; set; }
        IMessageRepository MessageRepository { get; set; }
        IUserChatRepository UserChatRepository { get; set; }
        IUserRepository UserRepository { get; set; }

        Task CreateTransaction();

        Task Commit();

        Task Rollback();

        Task Save();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.UserChat
{
    public interface IUserChatRepository : IGenericRepository<Entities.UserChat>
    {
        Task<Entities.UserChat> GetUserChat(string userId, int chatId);

        Task<Entities.AppUser> GetPartnerPrivateChat(string userId, int chatId);
    }
}
using Microsoft.EntityFrameworkCore;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.UserChat;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Repositories
{
    public class UserChatRepository : GenericRepository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(SocialDBContext context) : base(context)
        {
        }

        public async Task<AppUser> GetPartnerPrivateChat(string userId, int chatId)
        {
            return (await Context.UserChats.Include(x => x.User).Where(x => x.UserId != userId && x.ChatId == chatId).FirstOrDefaultAsync())?.User;
        }

        public async Task<UserChat> GetUserChat(string userId, int chatId)
        {
            return await Context.UserChats.Where(x => x.UserId == userId && x.ChatId == chatId).FirstOrDefaultAsync();
        }
    }
}
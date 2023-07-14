using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Social_Backend.Infrastructure.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(SocialDBContext context) : base(context)
        {
        }

        public IQueryable<Chat> GetChatsByUserId(string userId)
        {
            var chats = Context.Chats
                .Include(x => x.UserChats)
                .Where(x => x.UserChats.Any(a => a.UserId == userId))
                ?? throw new NotFoundException("Cannot find chats");

            return chats;
        }
    }
}
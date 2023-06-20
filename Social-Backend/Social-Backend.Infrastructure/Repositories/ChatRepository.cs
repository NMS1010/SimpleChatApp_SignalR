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

namespace Social_Backend.Infrastructure.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        private readonly SocialDBContext _context;

        public ChatRepository(IUnitOfWork<SocialDBContext> unitOfWork) : base(unitOfWork)
        {
        }

        public ChatRepository(SocialDBContext socialDBContext) : base(socialDBContext)
        {
            _context = socialDBContext;
        }

        public IQueryable<Chat> GetChatsByUserId(string userId)
        {
            var chats = _context.UserChats.Where(x => x.UserId == userId).Select(x => x.Chat) ?? throw new NotFoundException("Cannot find chats");

            return chats;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(SocialDBContext context) : base(context)
        {
        }

        public IQueryable<Message> GetMessagesByChat(int chatId)
        {
            var messages = Context.Messages
                .Include(x => x.User)
                .Include(x => x.Chat)
                .Where(x => x.ChatId == chatId)
                .OrderBy(x => x.CreateDate) ?? throw new NotFoundException("Cannot get messages for this chat");
            return messages;
        }
    }
}
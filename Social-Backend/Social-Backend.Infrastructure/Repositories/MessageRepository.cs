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
        private readonly SocialDBContext _context;

        public MessageRepository(IUnitOfWork<SocialDBContext> unitOfWork) : base(unitOfWork)
        {
        }

        public MessageRepository(SocialDBContext socialDBContext) : base(socialDBContext)
        {
            _context = socialDBContext;
        }

        public IQueryable<Message> GetMessagesByChat(int chatId)
        {
            var messages = _context.Messages
                .Where(x => x.ChatId == chatId)
                .OrderByDescending(x => x.CreateDate) ?? throw new NotFoundException("Cannot get messages for this chat");
            return messages;
        }
    }
}
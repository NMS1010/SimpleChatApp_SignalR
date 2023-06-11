using AutoMapper.Configuration.Conventions;
using Social_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.Message
{
    public interface IMessageRepository : IGenericRepository<Entities.Message>
    {
        IQueryable<Entities.Message> GetMessagesByChat(int chatId);
    }
}
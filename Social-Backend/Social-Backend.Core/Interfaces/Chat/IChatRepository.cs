using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.Chat
{
    public interface IChatRepository : IGenericRepository<Entities.Chat>
    {
        IQueryable<Entities.Chat> GetChatsByUserId(string userId);
    }
}
using Social_Backend.Application.Common.Models.Message;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.Message
{
    public interface IMessageService
    {
        Task CreateMessage(MessageCreateRequest request);

        Task<PaginatedResult<MessageDTO>> GetMessage(MessageGetPagingRequest request);
    }
}
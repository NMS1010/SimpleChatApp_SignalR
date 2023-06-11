using Social_Backend.Application.Common.Models.Chat;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.Chat
{
    public interface IChatService
    {
        Task<PaginatedResult<ChatDTO>> GetChatsByUser(ChatGetPagingRequest request);

        Task CreateRoom(ChatCreateRequest request);

        Task CreatePrivateRoom(ChatCreateRequest request);

        Task<ChatDTO> GetChatById(int chatId);
    }
}
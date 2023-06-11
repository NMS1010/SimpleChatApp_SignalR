using AutoMapper;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Chat;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IChatRoleRepository _chatRoleRepository;
        public readonly IUnitOfWork<SocialDBContext> _unitOfWork;

        public ChatService(IChatRepository chatRepository,
            IMapper mapper,
            IUnitOfWork<SocialDBContext> unitOfWork,
            IChatRoleRepository chatRoleRepository)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _chatRoleRepository = chatRoleRepository;
        }

        public async Task CreateRoom(ChatCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chat = new Chat()
                {
                    Name = request.Name,
                    ChatType = CHAT_TYPE.GROUP_TYPE
                };
                var chatRole = await _chatRoleRepository.GetByName(CHAT_ROLE.LEADER_ROLE);
                chat.UserChats.Add(new UserChat()
                {
                    UserId = request.UserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                await _chatRepository.Insert(chat);
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
            }
        }

        public async Task<PaginatedResult<ChatDTO>> GetChatsByUser(ChatGetPagingRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chats = _chatRepository.GetChatsByUserId(request.UserId);

                var res = await chats.PaginatedListAsync(request.PageIndex, request.PageSize);
                await _unitOfWork.Commit();
                return _mapper.Map<PaginatedResult<Chat>, PaginatedResult<ChatDTO>>(res);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<ChatDTO> GetChatById(int chatId)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chat = await _chatRepository.GetById(chatId);
                await _unitOfWork.Commit();
                return _mapper.Map<Chat, ChatDTO>(chat);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task CreatePrivateRoom(ChatCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chat = new Chat()
                {
                    ChatType = CHAT_TYPE.PRIVATE_TYPE
                };
                var chatRole = await _chatRoleRepository.GetByName(CHAT_ROLE.SAME_ROLE);
                chat.UserChats.Add(new UserChat()
                {
                    UserId = request.UserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                chat.UserChats.Add(new UserChat()
                {
                    UserId = request.RootUserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                await _chatRepository.Insert(chat);
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
            }
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Chat;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Core.Interfaces.User;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateRoom(ChatCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chat = new Chat()
                {
                    Name = request.Name,
                    ChatType = CHAT_TYPE.GROUP_TYPE,
                    Avatar = "default-group.png"
                };
                await _unitOfWork.ChatRepository.Insert(chat);
                await _unitOfWork.Save();
                var chatRole = await _unitOfWork.ChatRoleRepository.GetByName(CHAT_ROLE.LEADER_ROLE);
                await _unitOfWork.UserChatRepository.Insert(new UserChat()
                {
                    ChatId = chat.ChatId,
                    UserId = request.UserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                return chat.ChatId;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<PaginatedResult<ChatDTO>> GetChatsByUser(ChatGetPagingRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chats = _unitOfWork.ChatRepository.GetChatsByUserId(request.UserId);
                var res = await chats.PaginatedListAsync(request.PageIndex, request.PageSize);
                foreach (var c in res.Items)
                {
                    if (c.ChatType == CHAT_TYPE.PRIVATE_TYPE)
                    {
                        var userChat = await _unitOfWork.UserChatRepository.GetPartnerPrivateChat(request.UserId, c.ChatId);
                        if (userChat != null)
                        {
                            c.Avatar = userChat.Avatar;
                            c.Name = $"{userChat.FirstName} {userChat.LastName}";
                        }
                    }
                }
                await _unitOfWork.Commit();
                var t = res.Items.Select(x => _mapper.Map<Chat, ChatDTO>(x));
                return new PaginatedResult<ChatDTO>(t.ToList(), request.PageIndex, res.TotalCount, request.PageSize);
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
                var chat = await _unitOfWork.ChatRepository.GetById(chatId);
                await _unitOfWork.Commit();
                return _mapper.Map<Chat, ChatDTO>(chat);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<int> CreatePrivateRoom(ChatCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var user = await _unitOfWork.UserRepository.GetById(request.UserId);
                var chat = new Chat()
                {
                    ChatType = CHAT_TYPE.PRIVATE_TYPE,
                    Avatar = user.Avatar ?? "",
                    Name = $"{user.FirstName} {user.LastName}".Trim()
                };
                await _unitOfWork.ChatRepository.Insert(chat);
                await _unitOfWork.Save();
                var chatRole = await _unitOfWork.ChatRoleRepository.GetByName(CHAT_ROLE.SAME_ROLE);
                await _unitOfWork.UserChatRepository.Insert(new UserChat()
                {
                    ChatId = chat.ChatId,
                    UserId = request.UserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                await _unitOfWork.UserChatRepository.Insert(new UserChat()
                {
                    ChatId = chat.ChatId,
                    UserId = request.RootUserId,
                    ChatRoleId = chatRole.ChatRoleId
                });
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                return chat.ChatId;
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<bool> UserAlreadyInChat(string userId, string rootUserId)
        {
            var chatUser = _unitOfWork.ChatRepository.GetChatsByUserId(userId);
            var chatUserRoot = _unitOfWork.ChatRepository.GetChatsByUserId(rootUserId);
            var res = await chatUser.Intersect(chatUserRoot).ToListAsync();
            return res.Count > 0;
        }
    }
}
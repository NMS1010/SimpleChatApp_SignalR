using Social_Backend.Application.Common.Constants;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Core.Interfaces.UserChat;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services
{
    public class UserChatService : IUserChatService
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task JoinRoom(string userId, int chatId)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var chatRole = await _unitOfWork.ChatRoleRepository.GetByName(CHAT_ROLE.MEMBER_ROLE);
                var userChat = new UserChat()
                {
                    ChatId = chatId,
                    UserId = userId,
                    ChatRoleId = chatRole.ChatRoleId
                };
                await _unitOfWork.UserChatRepository.Insert(userChat);
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
            }
        }

        public async Task LeaveRoom(string userId, int chatId)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var userChat = await _unitOfWork.UserChatRepository.GetUserChat(userId, chatId);
                _unitOfWork.UserChatRepository.Delete(userChat);
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
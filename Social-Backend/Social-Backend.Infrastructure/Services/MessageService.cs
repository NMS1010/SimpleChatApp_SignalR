using AutoMapper;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Message;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Helpers;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Social_Backend.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public MessageService(IMapper mapper, IUnitOfWork unitOfWork, IUploadService uploadService, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
            _currentUserService = currentUserService;
        }

        public async Task<MessageDTO> CreateMessage(MessageCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var message = new Message()
                {
                    ChatId = request.ChatId,
                    Text = request.Text,
                    Status = request.Status,
                    User = await _unitOfWork.UserRepository.GetById(_currentUserService.UserId),
                    MessageType = MESSAGE_TYPE.TEXT
                };
                if (request.File != null)
                {
                    message.File = await _uploadService.UploadFile(request.File);
                    var type = MessageHelpers.GetMessageTypeFromFile(request.File);
                    if (type == MESSAGE_TYPE.IMAGE && message.MessageType != null &&
                        message.MessageType == MESSAGE_TYPE.TEXT)
                    {
                        message.MessageType = MESSAGE_TYPE.TEXT_IMAGE;
                    }
                    else
                    {
                        message.MessageType = type;
                    }
                }
                var chat = await _unitOfWork.ChatRepository.GetById(request.ChatId);
                chat.LastMessage = request.Text;
                chat.LastMessageDate = DateTime.Now;

                _unitOfWork.ChatRepository.Update(chat);
                await _unitOfWork.MessageRepository.Insert(message);

                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                return _mapper.Map<MessageDTO>(message);
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw new Exception("Cannot create message");
            }
        }

        public async Task<PaginatedResult<MessageDTO>> GetMessage(MessageGetPagingRequest request)
        {
            var messages = _unitOfWork.MessageRepository.GetMessagesByChat(request.ChatId);
            var res = await messages.PaginatedListAsync(request.PageIndex, request.PageSize);
            var t = res.Items.Select(x => _mapper.Map<Message, MessageDTO>(x)).ToList();

            return new PaginatedResult<MessageDTO>(t, request.PageIndex, res.TotalCount, request.PageSize);
        }
    }
}
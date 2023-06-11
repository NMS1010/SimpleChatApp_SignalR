using AutoMapper;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Message;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IUnitOfWork<SocialDBContext> _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork<SocialDBContext> unitOfWork, IUploadService uploadService)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public async Task CreateMessage(MessageCreateRequest request)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var message = new Message()
                {
                    ChatId = request.ChatId,
                    Image = await _uploadService.UploadFile(request.Image),
                    Text = request.Text,
                    Status = request.Status,
                };
                await _messageRepository.Insert(message);
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
            }
            catch
            {
                await _unitOfWork.Rollback();
            }
        }

        public async Task<PaginatedResult<MessageDTO>> GetMessage(MessageGetPagingRequest request)
        {
            var messages = _messageRepository.GetMessagesByChat(request.ChatId);
            var res = await messages.PaginatedListAsync(request.PageIndex, request.PageSize);

            return _mapper.Map<PaginatedResult<Message>, PaginatedResult<MessageDTO>>(res);
        }
    }
}
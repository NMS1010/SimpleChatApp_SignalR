using AutoMapper;
using Microsoft.AspNetCore.Http;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Common.Models.User;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedResult<UserDTO>> FindUsersByName(UserSearchRequest request)
        {
            var users = _unitOfWork.UserRepository.FindUsersByName(request);
            var res = await users.PaginatedListAsync(request.PageIndex, request.PageSize);
            var t = res.Items.Where(x => x.Id != _currentUserService.UserId).Select(x => _mapper.Map<AppUser, UserDTO>(x)).ToList();
            return new PaginatedResult<UserDTO>(t, res.PageIndex, res.TotalCount, request.PageSize);
        }

        public async Task<UserDTO> GetById(string id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
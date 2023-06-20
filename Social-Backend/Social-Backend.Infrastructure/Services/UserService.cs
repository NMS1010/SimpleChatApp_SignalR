using AutoMapper;
using Social_Backend.Application.Common.Extentions;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Common.Models.User;
using Social_Backend.Application.Dtos;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<UserDTO>> FindUsersByName(UserSearchRequest request)
        {
            var users = _userRepository.FindUsersByName(request);
            var res = await users.PaginatedListAsync(request.PageIndex, request.PageSize);
            return _mapper.Map<PaginatedResult<UserDTO>>(res);
        }

        public async Task<UserDTO> GetById(string id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
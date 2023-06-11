using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException("Cannot find this user");

            return _mapper.Map<UserDTO>(user);
        }
    }
}
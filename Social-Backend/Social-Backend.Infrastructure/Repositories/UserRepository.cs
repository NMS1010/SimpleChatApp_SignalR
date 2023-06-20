using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Common.Models.User;
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

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<AppUser> FindUsersByName(UserSearchRequest request)
        {
            return _userManager.Users.Where(x => x.FirstName.Contains(request.Keyword) || x.LastName.Contains(request.Keyword));
        }

        public async Task<AppUser> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException("Cannot find this user");

            return user;
        }
    }
}
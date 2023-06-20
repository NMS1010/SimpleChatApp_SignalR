using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Common.Models.User;
using Social_Backend.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.User
{
    public interface IUserService
    {
        Task<UserDTO> GetById(string id);

        Task<PaginatedResult<UserDTO>> FindUsersByName(UserSearchRequest request);
    }
}
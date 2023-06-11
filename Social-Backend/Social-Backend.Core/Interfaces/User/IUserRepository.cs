using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.User
{
    public interface IUserRepository
    {
        Task<UserDTO> GetById(string id);
    }
}
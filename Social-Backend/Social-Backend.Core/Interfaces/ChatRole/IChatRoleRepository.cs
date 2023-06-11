using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.ChatRole
{
    public interface IChatRoleRepository : IGenericRepository<Entities.ChatRole>
    {
        Task<Entities.ChatRole> GetByName(string name);
    }
}
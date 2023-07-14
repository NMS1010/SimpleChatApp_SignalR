using Microsoft.EntityFrameworkCore;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Repositories
{
    public class ChatRoleRepository : GenericRepository<ChatRole>, IChatRoleRepository
    {
        public ChatRoleRepository(SocialDBContext context) : base(context)
        {
        }

        public async Task<ChatRole> GetByName(string name)
        {
            var res = await Context.ChatRoles.Where(x => x.ChatRoleName == name).SingleOrDefaultAsync();
            return res;
        }
    }
}
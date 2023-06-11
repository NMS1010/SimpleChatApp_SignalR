using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.UserChat;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Repositories
{
    public class UserChatRepository : GenericRepository<UserChat>, IUserChatRepository
    {
        public UserChatRepository(IUnitOfWork<SocialDBContext> unitOfWork) : base(unitOfWork)
        {
        }

        public UserChatRepository(SocialDBContext socialDBContext) : base(socialDBContext)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Dtos
{
    public class UserChatDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public int ChatId { get; set; }
        public int ChatRoleId { get; set; }
        public string ChatRoleName { get; set; }
    }
}
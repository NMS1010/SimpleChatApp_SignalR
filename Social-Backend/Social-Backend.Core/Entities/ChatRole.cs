using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Entities
{
    public class ChatRole
    {
        public int ChatRoleId { get; set; }
        public string ChatRoleName { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Constants
{
    public class CHAT_ROLE
    {
        public static readonly string MEMBER_ROLE = "Member";
        public static readonly string LEADER_ROLE = "Leader";
        public static readonly string SAME_ROLE = "Same Level";

        public static readonly List<string> ChatRoles = new()
            {
                MEMBER_ROLE,LEADER_ROLE
            };
    }
}
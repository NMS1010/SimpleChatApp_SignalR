using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Constants
{
    public class CHAT_TYPE
    {
        public static readonly string PRIVATE_TYPE = "Private";
        public static readonly string GROUP_TYPE = "Group";

        public static readonly List<string> ChatTypes = new()
            {
                PRIVATE_TYPE,GROUP_TYPE
            };
    }
}
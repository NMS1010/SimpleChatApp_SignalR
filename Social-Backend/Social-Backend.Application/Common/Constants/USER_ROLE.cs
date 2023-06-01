using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Constants
{
    public class USER_ROLE
    {
        public static readonly string CUSTOMER_ROLE = "Customer";
        public static readonly string ADMIN_ROLE = "Admin";

        public static readonly List<string> Roles = new()
            {
                ADMIN_ROLE,CUSTOMER_ROLE
            };
    }
}
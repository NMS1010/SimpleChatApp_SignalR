using Microsoft.AspNetCore.Identity;
using Social_Backend.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredTime { get; set; }

        public ICollection<UserChat> UserChats { get; set; }
    }
}
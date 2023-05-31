using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Entities
{
    public class UserChat
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
using Social_Backend.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Entities
{
    public class Chat : AuditableEntity
    {
        public int ChatId { get; set; }
        public string ChatType { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
    }
}
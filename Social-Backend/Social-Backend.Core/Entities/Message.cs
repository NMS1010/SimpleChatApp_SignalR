using Social_Backend.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Entities
{
    public class Message : AuditableEntity
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
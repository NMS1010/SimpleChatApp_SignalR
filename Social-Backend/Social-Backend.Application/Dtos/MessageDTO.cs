using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Dtos
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public bool IsMe { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        public string File { get; set; }
        public string MessageType { get; set; }
        public string Creator { get; set; }
        public string UserAvatar { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
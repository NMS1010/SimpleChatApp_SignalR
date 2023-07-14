using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Dtos
{
    public class ChatDTO
    {
        public int ChatId { get; set; }

        public string ChatType { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string Avatar { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
        public List<string> MemberIds { get; set; }
        public bool OnlyMe { get; set; }
        public string RoomId { get; set; }
        public List<MessageDTO> Messages { get; set; }
        public List<UserChatDTO> UserChats { get; set; }
    }
}
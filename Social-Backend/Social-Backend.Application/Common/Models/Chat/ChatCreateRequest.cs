using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Chat
{
    public class ChatCreateRequest
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string RootUserId { get; set; }
    }
}
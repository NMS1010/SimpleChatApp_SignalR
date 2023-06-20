using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Message
{
    public class MessageCreateRequest
    {
        public string Text { get; set; }
        public int Status { get; set; }
        public IFormFile Image { get; set; }
        public int ChatId { get; set; }
        public string RoomId { get; set; }
    }
}
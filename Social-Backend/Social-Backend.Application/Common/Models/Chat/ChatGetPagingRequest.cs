using Social_Backend.Application.Common.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Chat
{
    public class ChatGetPagingRequest : PagingRequest
    {
        public string UserId { get; set; }
    }
}
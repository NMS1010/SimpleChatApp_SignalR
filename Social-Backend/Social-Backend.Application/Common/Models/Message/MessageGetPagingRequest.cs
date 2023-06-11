using Social_Backend.Application.Common.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Message
{
    public class MessageGetPagingRequest : PagingRequest
    {
        public int ChatId { get; set; }
    }
}
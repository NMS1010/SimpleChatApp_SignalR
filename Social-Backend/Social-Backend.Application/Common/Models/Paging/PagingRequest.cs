using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Paging
{
    public class PagingRequest
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 1000;

        public string ColumnName { get; set; }
        public string TypeSort { get; set; } = "ASC";
        public int SortBy { get; set; }
    }
}
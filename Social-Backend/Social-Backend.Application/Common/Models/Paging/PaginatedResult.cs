using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Paging
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public PaginatedResult(List<T> items, int pageIndex, int totalCount, int pageSize)
        {
            var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = totalPage;
            HasPreviousPage = pageIndex > 1;
            HasNextPage = pageIndex < totalPage;
        }

        public static async Task<PaginatedResult<T>> CreatePaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>(items, pageIndex, count, pageSize);
        }
    }
}
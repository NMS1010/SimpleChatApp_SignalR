using Social_Backend.Application.Common.Models.Paging;

namespace Social_Backend.Application.Common.Extentions
{
    public static class PaginatedMappingExtensions
    {
        public static Task<PaginatedResult<T>> PaginatedListAsync<T>(this IQueryable<T> queryable, int pageIndex, int pageSize) =>
            PaginatedResult<T>.CreatePaginatedList(queryable, pageIndex, pageSize);
    }
}
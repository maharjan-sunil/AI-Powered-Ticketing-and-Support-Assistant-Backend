using AI_Powered_Ticketing_and_Support_System_Backend.Feature.Base.Model;
using Microsoft.EntityFrameworkCore;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Base
{

    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            PaginationParams pagination,
            CancellationToken ct = default)
        {
            var page = Math.Max(1, pagination.Page);
            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(ct);

            return new PagedResult<T>(items, page, pagination.PageSize, totalCount);
        }
    }

}

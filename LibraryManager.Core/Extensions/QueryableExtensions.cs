namespace LibraryManager.Core.Extensions
{
    using LibraryManager.Core.ValueObjects.Filters;
    using System.Linq;

    public static class QueryableExtensions
    {
        public static IQueryable<T> SetPagination<T>(this IQueryable<T> query, BaseFilter filter)
        {
            return query
                .Skip((filter.Offset - 1) * filter.Limit)
                .Take(filter.Limit);
        }
    }
}

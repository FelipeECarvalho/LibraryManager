namespace LibraryManager.Core.ValueObjects.Filters
{
    public sealed record AuthorFilter
        : BaseFilter
    {
        public AuthorFilter(int limit, int offset)
            : base(limit, offset) 
        {
        }
    }
}

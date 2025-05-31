namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.ValueObjects.Filters;

    public sealed record GetAuthorsQuery(int Limit = 10, int Offset = 1)
        : IQuery<IList<AuthorResponse>>
    {
        public AuthorFilter ToFilter() 
            => new(Limit, Offset);
    }
}

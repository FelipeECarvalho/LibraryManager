namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;

    public sealed record GetAuthorsQuery(int Limit = 10, int Offset = 1)
        : IQuery<IList<AuthorResponse>>;
}

namespace LibraryManager.Application.Queries.Author.GetAll
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetAuthorsQuery : IQuery<IList<AuthorResponse>>;
}

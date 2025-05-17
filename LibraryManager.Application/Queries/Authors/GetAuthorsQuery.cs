namespace LibraryManager.Application.Queries.Authors
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetAuthorsQuery : IQuery<IList<AuthorResponse>>;
}

namespace LibraryManager.Application.Queries.Author.GetAuthorById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetAuthorByIdQuery(Guid Id) 
        : IQuery<AuthorResponse>;
}

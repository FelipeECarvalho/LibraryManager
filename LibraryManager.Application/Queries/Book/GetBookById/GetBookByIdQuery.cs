namespace LibraryManager.Application.Queries.Book.GetBookById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System;

    public sealed record GetBookByIdQuery(Guid Id)
        : IQuery<BookResponse>;
}

namespace LibraryManager.Application.Queries.Book.GetBooks
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Book;
    using System.Collections.Generic;

    public sealed record GetBooksQuery
        : IQuery<IList<BookResponse>>;
}

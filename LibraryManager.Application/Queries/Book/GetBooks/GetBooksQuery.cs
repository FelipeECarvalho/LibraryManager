namespace LibraryManager.Application.Queries.Book.GetBooks
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Book;
    using System.Collections.Generic;

    public sealed record GetBooksQuery(int Limit = 100, int Offset = 1, string Title = null)
        : IQuery<IList<BookResponse>>;
}

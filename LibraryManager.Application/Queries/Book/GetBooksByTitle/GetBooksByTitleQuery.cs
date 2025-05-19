namespace LibraryManager.Application.Queries.Book.GetBooksByTitle
{
    using LibraryManager.Application.Abstractions.Messaging;
    using System.Collections.Generic;

    public sealed record GetBooksByTitleQuery(string Title)
        : IQuery<IList<BookResponse>>;
}

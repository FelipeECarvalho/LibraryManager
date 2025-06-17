namespace LibraryManager.Application.Commands.Book.CreateBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Book;

    public sealed record CreateBookCommand(
        string Title,
        string Description,
        int? StockNumber,
        DateTimeOffset PublicationDate,
        string Isbn,
        Guid AuthorId,
        Guid LibraryId) : ICommand<BookResponse>;
}

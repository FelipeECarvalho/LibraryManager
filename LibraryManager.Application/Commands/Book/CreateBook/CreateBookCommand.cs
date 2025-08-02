namespace LibraryManager.Application.Commands.Book.CreateBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Book;
    using System.Text.Json.Serialization;

    public sealed record CreateBookCommand(
        string Title,
        string Description,
        int? StockNumber,
        DateTimeOffset PublicationDate,
        string Isbn,
        Guid AuthorId) : ICommand<BookResponse>
    {
        [JsonIgnore]
        public Guid LibraryId { get; set; }
    }
}

namespace LibraryManager.Application.Queries.Book
{
    using LibraryManager.Application.Queries.Author;

    public class BookResponse
    {
        public Guid Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public DateTimeOffset PublicationDate { get; init; }

        public string ISBN { get; init; }

        public int? StockNumber { get; init; }

        public AuthorResponse Author { get; init; }

        public static BookResponse FromEntity(Core.Entities.Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                StockNumber = book.StockNumber,
                Author = AuthorResponse.FromEntity(book.Author)
            };
        }
    }
}
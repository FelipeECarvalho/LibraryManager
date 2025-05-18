namespace LibraryManager.Application.DTOs
{
    using LibraryManager.Application.Queries.Author;

    public class BookDto
    {
        public Guid Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public DateTime PublicationDate { get; init; }

        public string ISBN { get; init; }

        public int StockNumber { get; init; }

        public AuthorResponse Author { get; init; }
    }
}
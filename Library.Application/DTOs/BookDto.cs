namespace Library.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; init; }
        public string Title { get; init; }

        public string Description { get; init; }

        public DateTime PublicationDate { get; init; }

        public string ISBN { get; init; }

        public int StockNumber { get; init; }

        public AuthorDto Author { get; init; }
    }
}
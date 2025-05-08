namespace Library.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; }
        public string Title { get; }

        public string Description { get; }

        public DateTime PublicationDate { get; }

        public string ISBN { get; }

        public int StockNumber { get; }

        public AuthorDto Author { get; }
    }
}
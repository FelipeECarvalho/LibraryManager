namespace Library.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }

        public string ISBN { get; set; }

        public int StockNumber { get; set; }

        public AuthorDto Author { get; set; }
    }
}
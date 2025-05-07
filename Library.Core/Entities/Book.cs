namespace Library.Core.Entities
{
    public class Book : BaseEntity
    {
        public Book()
        {
        }

        public Book(string title, string description, DateTime publicationDate, string isbn, int stockNumber, int authorId) : base()
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ISBN = isbn;
            StockNumber = stockNumber;
            AuthorId = authorId;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime PublicationDate { get; private set; }

        public string ISBN { get; init; }

        public int StockNumber { get; private set; }

        public int AuthorId { get; init; }

        public Author Author { get; }

        public void Update(string title, string description, DateTime publicationDate)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            UpdateDate = DateTime.Now;
        }

        public void UpdateStock(int stockNumber)
        {
            StockNumber = stockNumber;
            UpdateDate = DateTime.Now;
        }
    }
}

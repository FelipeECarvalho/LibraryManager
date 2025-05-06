namespace Library.Core.Entities
{
    public class Book : BaseEntity
    {
        public Book(string title, string description, DateTime publicationDate, string isbn, int stockNumber, int authorId) : base()
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ISBN = isbn;
            StockNumber = stockNumber;
            AuthorId = authorId;
        }

        public string Title { get; init; }

        public string Description { get; private set; }

        public DateTime PublicationDate { get; init; }

        public string ISBN { get; init; }

        public int StockNumber { get; private set; }

        public int AuthorId { get; init; }

        public Author Author { get; }

        public void Update(string description)
        {
            Description = description;
            UpdateDate = DateTime.Now;
        }

        public void UpdateStock(int stockNumber)
        {
            StockNumber = stockNumber;
            UpdateDate = DateTime.Now;
        }
    }
}

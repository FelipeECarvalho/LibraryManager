namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Errors;

    public class Book : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Book()
            : base()
        {
        }

        public Book(string title, string description, DateTimeOffset publicationDate, string isbn, int? stockNumber, Guid authorId)
            : base()
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            ISBN = isbn;
            StockNumber = stockNumber;
            AuthorId = authorId;
        }

        public string Title { get; private set; }

        public string? Description { get; private set; }

        public DateTimeOffset PublicationDate { get; private set; }

        public string ISBN { get; private set; }

        public int? StockNumber { get; private set; }

        public Guid AuthorId { get; private set; }

        public Author Author { get; private set; }

        public IList<BookCategory> BookCategories { get; private set; }

        public IList<Loan>? Loans { get; private set; }

        public void Update(string title, string description, DateTimeOffset publicationDate)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
        }

        public void UpdateStock(int stockNumber)
        {
            if (StockNumber < 0) 
            {
                throw new ArgumentException(DomainErrors.Book.StockNumberInvalid.Description);
            }

            StockNumber = stockNumber;
        }
    }
}

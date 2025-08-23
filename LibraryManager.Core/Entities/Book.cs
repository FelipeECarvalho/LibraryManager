namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Extensions;

    public class Book : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Book()
            : base()
        {
        }

        public Book(
            string title,
            string description,
            DateTimeOffset publicationDate,
            string isbn,
            int? stockNumber,
            Guid authorId,
            Guid libraryId)
            : base()
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            Isbn = isbn;
            StockNumber = stockNumber;
            AuthorId = authorId;
            LibraryId = libraryId;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTimeOffset PublicationDate { get; private set; }

        public string Isbn { get; private set; }

        public int? StockNumber { get; private set; }

        public Guid AuthorId { get; private set; }

        public Author Author { get; private set; }

        public IList<BookCategory> BookCategories { get; private set; } = [];

        public IList<Loan> Loans { get; private set; } = [];

        public Guid LibraryId { get; private set; }

        public Library Library { get; private set; }

        public void Update(string title, string description, DateTimeOffset publicationDate)
        {
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            UpdateDate = DateTimeOffset.UtcNow;
        }

        public bool IsAvailable()
        {
            if (StockNumber is null || !Loans.Any())
            {
                return true;
            }

            var totalUnavailable = Loans
                .Count(x => x.Status.IsBookUnavailable());

            return StockNumber > totalUnavailable;
        }

        public Result UpdateStock(int stockNumber)
        {
            if (stockNumber < 0)
            {
                return Result.Failure(DomainErrors.Book.StockNumberInvalid);
            }

            StockNumber = stockNumber;
            UpdateDate = DateTimeOffset.UtcNow;

            return Result.Success();
        }
    }
}

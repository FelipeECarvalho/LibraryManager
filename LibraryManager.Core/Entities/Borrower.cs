namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Extensions;
    using LibraryManager.Core.ValueObjects;

    public class Borrower : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Borrower()
            : base()
        {
        }

        public Borrower(Name name, string document, string email, DateTimeOffset birthDate, Address address)
            : base()
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            BirthDate = birthDate;
        }

        public Name Name { get; private set; }

        public string Document { get; private set; }

        public string Email { get; private set; }

        public DateTimeOffset BirthDate { get; private set; }

        public Address Address { get; private set; }

        public IList<Loan> Loans { get; private set; } = [];

        public Guid LibraryId { get; private set; }

        public Library Library { get; private set; }

        public bool CanLoan(Book book)
        {
            return !Loans
                .Any(x => x.BookId == book.Id && x.Status.IsBookUnavailable());
        }

        public void Update(Name name, Address address)
        {
            Name = name;
            Address = address;
            UpdateDate = DateTimeOffset.Now;
        }
    }
}

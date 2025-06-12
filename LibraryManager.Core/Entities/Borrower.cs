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

        public Borrower(Name name, Email email, string document, DateTimeOffset birthDate, Guid libraryId, Address address)
        {
            Document = document;
            Address = address;
            BirthDate = birthDate;
            LibraryId = libraryId;
            Name = name;
            Email = email;

        }

        public Name Name { get; protected set; }

        public Email Email { get; protected set; }

        public string Document { get; private set; }

        public DateTimeOffset BirthDate { get; private set; }

        public Address Address { get; private set; }

        public Guid LibraryId { get; private set; }

        public Library Library { get; private set; }

        public IList<Loan> Loans { get; private set; } = [];

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

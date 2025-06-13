namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.ValueObjects;

    public class Library : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Library()
            : base()
        {
        }

        public Library(string name, Address address, TimeOnly? openingTime, TimeOnly? closingTime)
        {
            Name = name;
            Address = address;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }

        public string Name { get; private set; }

        public Address Address { get; private set; }

        public TimeOnly? OpeningTime { get; private set; }

        public TimeOnly? ClosingTime { get; private set; }

        public IList<Book> Books { get; private set; }

        public IList<User> Users { get; private set; }

        public IList<Borrower> Borrowers { get; private set; }

        public IList<Category> Categories { get; private set; }
    }
}

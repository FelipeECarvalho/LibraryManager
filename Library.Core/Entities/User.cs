namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.ValueObjects;

    public class User : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private User()
            : base()
        {
        }

        public User(Name name, string document, string email, DateTimeOffset birthDate, Address address)
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

        public DateTimeOffset BirthDate { get; set; }

        public Address Address { get; private set; }

        public void Update(Name name, Address address)
        {
            Name = name;
            Address = address;
        }
    }
}

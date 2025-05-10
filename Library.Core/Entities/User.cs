namespace Library.Core.Entities
{
    using Library.Core.ValueObjects;

    public class User : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private User() 
            : base()
        {
        }

        public User(Name name, string document, string email, Address address) 
            : base()
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
        }

        public Name Name { get; private set; }

        public string Document { get; private set; }

        public string Email { get; private set; }

        public Address Address { get; private set; }

        public void Update(Name name, Address address)
        {
            Name = name;
        }
    }
}

namespace Library.Core.Entities
{
    public class User : BaseEntity
    {
        private User() { }

        public User(string name, string document, string email) : base()
        {
            Name = name;
            Document = document;
            Email = email;
        }

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }

        public void Update(string name)
        {
            Name = name;
            UpdateDate = DateTime.Now;
        }
    }
}

namespace Library.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string document, string email) : base()
        {
            Name = name;
            Document = document;
            Email = email;
        }

        public string Name { get; private set; }
        public string Document { get; init; }
        public string Email { get; init; }

        public void Update(string name)
        {
            Name = name;
            UpdateDate = DateTime.Now;
        }
    }
}

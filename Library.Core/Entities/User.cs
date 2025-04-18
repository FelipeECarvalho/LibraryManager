namespace Library.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }

        public void Update(string name, string email)
        {
            Name = name;
            Document = email;
        }
    }
}

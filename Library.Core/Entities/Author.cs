namespace Library.Core.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Book> Books { get; set; }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            UpdateDate = DateTime.Now;
        }
    }
}

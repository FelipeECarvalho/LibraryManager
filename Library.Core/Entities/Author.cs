namespace Library.Core.Entities
{
    public class Author : BaseEntity
    {
        public Author()
        {
        }

        public Author(string name) : base()
        {
            Name = name;
        }

        public Author(string name, string description, IList<Book> books)
        {
            Name = name;
            Description = description;
            Books = books;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public IList<Book> Books { get; private set; }

        public void AddBook(Book book)
        {
            Books ??= [];
            Books.Add(book);

            UpdateDate = DateTime.Now;
        }

        public void Update(string name)
        {
            Name = name;
            UpdateDate = DateTime.Now;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            UpdateDate = DateTime.Now;
        }
    }
}

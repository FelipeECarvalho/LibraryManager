namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.ValueObjects;

    public class Author : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Author()
            : base()
        {
        }

        public Author(Name name)
            : base()
        {
            Name = name;
            Books = [];
        }

        public Author(Name name, string? description, IList<Book> books)
            : base()
        {
            Name = name;
            Books = books;
            Description = description;
        }

        public Name Name { get; private set; }

        public string? Description { get; private set; }

        public IList<Book>? Books { get; private set; }

        public void AddBook(IList<Book> books)
        {
            Books ??= [];
            foreach (var book in books ?? [])
            {
                Books.Add(book);
            }
        }

        public void AddBook(Book book)
        {
            Books ??= [];
            Books.Add(book);
        }

        public void Update(Name name)
        {
            Name = name;
        }

        public void Update(Name name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}

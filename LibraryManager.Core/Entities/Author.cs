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

        public Author(Name name, string? description = null)
            : base()
        {
            Books = [];
            Name = name;
            Description = description;
        }

        public Name Name { get; private set; }

        public string? Description { get; private set; }

        public IList<Book>? Books { get; private set; }

        public void AddBook(IList<Book> books)
        {
            Books ??= [];
            foreach (var book in books)
            {
                Books.Add(book);
            }

            UpdateDate = DateTimeOffset.Now;
        }

        public void Update(Name name, string? description)
        {
            Name = name;
            Description = description;

            UpdateDate = DateTimeOffset.Now;
        }
    }
}

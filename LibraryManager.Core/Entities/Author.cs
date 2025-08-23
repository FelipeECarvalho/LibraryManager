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

        public Author(Name name, string description = null)
            : base()
        {
            Name = name;
            Description = description;
        }

        public Name Name { get; private set; }

        public string Description { get; private set; }

        public IList<Book> Books { get; private set; }

        public void Update(Name name, string description)
        {
            Name = name;
            Description = description;
            UpdateDate = DateTimeOffset.UtcNow;
        }
    }
}

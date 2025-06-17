namespace LibraryManager.Core.Entities
{
    public class Category : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Category()
            : base()
        {
        }

        public Category(string name, string description, Guid libraryId)
            : base()
        {
            Name = name;
            Description = description;
            LibraryId = libraryId;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public IList<BookCategory> BookCategories { get; private set; }

        public Guid LibraryId { get; private set; }

        public Library Library { get; private set; }
    }
}

namespace LibraryManager.Core.Entities
{
    public class Category : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Category() 
            : base()
        {
        }

        public Category(string name)
            : base()
        {
            Name = name;
        }

        public Category(string name, string description) 
            : base()
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public IList<BookCategory>? BookCategories { get; private set; }
    }
}

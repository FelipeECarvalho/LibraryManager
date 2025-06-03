namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.ValueObjects;

    public class Library : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Library()
            : base()
        {
        }

        public string Name { get; private set; }
        
        public Address Address { get; private set; }

        public TimeOnly OpeningTime { get; private set; }

        public TimeOnly ClosingTime { get; private set; }

        public IList<Book> Books { get; private set; }

        public IList<User> Users { get; private set; }

        public IList<Category> Categories { get; private set; }
    }
}

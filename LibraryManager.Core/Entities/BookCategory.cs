namespace LibraryManager.Core.Entities
{
    public class BookCategory : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private BookCategory()
            : base()
        {
        }

        public BookCategory(Guid bookId, Guid categoryId)
            : base()
        {
            BookId = bookId;
            CategoryId = categoryId;
        }

        public Guid BookId { get; private set; }

        public Book Book { get; private set; }

        public Guid CategoryId { get; private set; }

        public Category Category { get; private set; }
    }
}

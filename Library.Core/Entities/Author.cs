namespace Library.Core.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Book> Books { get; set; }
    }
}

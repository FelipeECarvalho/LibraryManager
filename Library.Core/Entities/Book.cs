namespace Library.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }

        public string ISBN { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public void Update(string title, string description, DateTime publicationDate)
        {
            Title = title;
            UpdateDate = DateTime.Now;
            Description = description;
            PublicationDate = publicationDate;
        }
    }
}

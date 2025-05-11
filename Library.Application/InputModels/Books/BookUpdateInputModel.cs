namespace Library.Application.InputModels.Books
{
    public class BookUpdateInputModel
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public DateTime PublicationDate { get; init; }
    }
}

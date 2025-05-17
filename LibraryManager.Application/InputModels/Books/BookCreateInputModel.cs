namespace LibraryManager.Application.InputModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookCreateInputModel
    {
        [Required]
        [Length(3, 255)]
        public string Title { get; init; }

        [Length(3, 255)]
        public string Description { get; init; }

        public int? StockNumber { get; init; }

        [Required]
        public DateTime PublicationDate { get; init; }

        [Required]
        public string ISBN { get; init; }

        [Required]
        public Guid AuthorId { get; init; }
    }
}

namespace Library.Application.InputModels.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookCreateInputModel
    {
        [Required]
        [Length(3, 255)]
        public string Title { get; set; }

        [Length(3, 255)]
        public string Description { get; set; }

        public int? StockNumber { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}

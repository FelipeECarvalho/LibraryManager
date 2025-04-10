using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Books
{
    public class BookCreateInputModel
    {
        [Required]
        [Length(3, 255)]
        public string Title { get; set; }

        [Required]
        [Length(3, 255)]
        public string Description { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string AuthorId { get; set; }
    }
}

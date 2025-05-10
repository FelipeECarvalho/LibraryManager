namespace Library.Application.InputModels.Authors
{
    using Library.Core.ValueObjects;
    using System.ComponentModel.DataAnnotations;

    public class AuthorCreateInputModel
    {
        [Required]
        public Name Name { get; set; }

        [StringLength(256, ErrorMessage = "Description cannot be more than 256 characters long")]
        public string Description { get; set; }
    }
}
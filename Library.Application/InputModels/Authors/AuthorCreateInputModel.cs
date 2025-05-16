namespace LibraryManager.Application.InputModels.Authors
{
    using LibraryManager.Core.ValueObjects;
    using System.ComponentModel.DataAnnotations;

    public class AuthorCreateInputModel
    {
        [Required]
        public Name Name { get; init; }

        [StringLength(256, ErrorMessage = "Description cannot be more than 256 characters long")]
        public string Description { get; init; }
    }
}
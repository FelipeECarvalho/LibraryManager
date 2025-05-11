namespace Library.Application.InputModels.Users
{
    using Library.Core.ValueObjects;
    using System.ComponentModel.DataAnnotations;

    public class UserCreateInputModel
    {
        [Required]
        public Name Name { get; init; }

        [Required]
        [Length(11, 11, ErrorMessage = "The document must have 11 characters long")]
        public string Document { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public Address Address { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Users
{
    public class UserCreateInputModel
    {
        [Required]
        [Length(2, 100, ErrorMessage = "The name must have between 2 and 100 characters")]
        public string Name { get; set; }

        [Required]
        [Length(11, 11, ErrorMessage = "The document must have 11 characters")]
        public string Document { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

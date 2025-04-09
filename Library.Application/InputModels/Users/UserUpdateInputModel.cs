using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Users
{
    public class UserUpdateInputModel
    {
        [Required]
        [Length(2, 100, ErrorMessage = "The name must have between 2 and 100 characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

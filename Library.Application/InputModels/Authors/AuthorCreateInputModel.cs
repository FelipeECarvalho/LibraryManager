using System.ComponentModel.DataAnnotations;

namespace Library.Application.InputModels.Authors
{
    public class AuthorCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
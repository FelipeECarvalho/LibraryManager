namespace LibraryManager.Application.InputModels.Authors
{
    using System.ComponentModel.DataAnnotations;

    public class AuthorAddBookInputModel
    {
        [Required]
        public IList<Guid> BookIds { get; init; }
    }
}
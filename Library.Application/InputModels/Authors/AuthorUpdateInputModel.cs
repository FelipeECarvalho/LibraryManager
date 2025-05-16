namespace LibraryManager.Application.InputModels.Authors
{
    using LibraryManager.Core.ValueObjects;

    public class AuthorUpdateInputModel
    {
        public Name Name { get; init; }

        public string Description { get; init; }
    }
}
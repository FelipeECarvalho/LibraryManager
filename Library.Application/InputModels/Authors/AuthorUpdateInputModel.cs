namespace Library.Application.InputModels.Authors
{
    using Library.Core.ValueObjects;

    public class AuthorUpdateInputModel
    {
        public Name Name { get; init; }

        public string Description { get; init; }
    }
}
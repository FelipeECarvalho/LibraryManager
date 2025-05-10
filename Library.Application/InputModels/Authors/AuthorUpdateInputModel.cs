namespace Library.Application.InputModels.Authors
{
    using Library.Core.ValueObjects;

    public class AuthorUpdateInputModel
    {
        public Name Name { get; set; }

        public string Description { get; set; }
    }
}
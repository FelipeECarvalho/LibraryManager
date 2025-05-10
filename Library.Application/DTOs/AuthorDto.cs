namespace Library.Application.DTOs
{
    using Library.Core.ValueObjects;

    public class AuthorDto
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Description { get; init; }
    }
}

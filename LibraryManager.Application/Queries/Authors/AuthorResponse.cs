namespace LibraryManager.Application.Queries.Authors
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects;

    public class AuthorResponse
    {
        public Guid Id { get; init; }

        public Name Name { get; init; }

        public string Description { get; init; }

        public static AuthorResponse FromEntity(Author author)
        {
            return new AuthorResponse
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description
            };
        }
    }
}

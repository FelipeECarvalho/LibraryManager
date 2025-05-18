namespace LibraryManager.Application.Queries.Author
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects;

    public sealed record AuthorResponse(Guid Id, Name Name, string Description)
    {
        public static AuthorResponse FromEntity(Author author)
            => new(author.Id, author.Name, author.Description);
    }
}

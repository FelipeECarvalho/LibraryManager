namespace LibraryManager.Application.Commands.Author.CreateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.ValueObjects;

    public sealed record CreateAuthorCommand(Name Name, string Description)
        : ICommand<AuthorResponse>;
}

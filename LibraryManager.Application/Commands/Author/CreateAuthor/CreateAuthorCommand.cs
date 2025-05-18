namespace LibraryManager.Application.Commands.Author.CreateAuthor
{
    using LibraryManager.Application.Queries.Author;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.ValueObjects;
    using MediatR;

    public sealed record CreateAuthorCommand(Name Name, string Description)
        : IRequest<Result<AuthorResponse>>;
}

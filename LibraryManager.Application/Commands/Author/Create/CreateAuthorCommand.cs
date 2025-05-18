namespace LibraryManager.Application.Commands.Author.Create
{
    using LibraryManager.Application.Queries.Author.GetAll;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.ValueObjects;
    using MediatR;

    public sealed record CreateAuthorCommand(Name Name, string Description)
        : IRequest<Result<AuthorResponse>>;
}

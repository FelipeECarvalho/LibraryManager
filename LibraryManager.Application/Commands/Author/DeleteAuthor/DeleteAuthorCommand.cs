namespace LibraryManager.Application.Commands.Author.DeleteAuthor
{
    using LibraryManager.Core.Common;
    using MediatR;

    public sealed record DeleteAuthorCommand(Guid Id) 
        : IRequest<Result>;
}

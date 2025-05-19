namespace LibraryManager.Application.Commands.Author.DeleteAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record DeleteAuthorCommand(Guid Id) 
        : ICommand;
}

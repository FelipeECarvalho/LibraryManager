namespace LibraryManager.Application.Commands.Book.DeleteBook
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record DeleteBookCommand(Guid Id) 
        : ICommand;
}

namespace LibraryManager.Application.Commands.Borrower.DeleteBorrower
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record DeleteBorrowerCommand(Guid Id)
        : ICommand;
}

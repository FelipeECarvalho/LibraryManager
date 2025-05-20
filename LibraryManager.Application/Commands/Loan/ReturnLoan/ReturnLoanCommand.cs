namespace LibraryManager.Application.Commands.Loan.ReturnLoan
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record ReturnLoanCommand(Guid Id)
        : ICommand;
}

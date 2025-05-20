namespace LibraryManager.Application.Commands.Loan.CreateLoan
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record CreateLoanCommand(
        Guid UserId,
        Guid BookId,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate) : ICommand<LoanResponse>;
}

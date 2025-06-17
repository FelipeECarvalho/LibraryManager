namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetLoansQuery(Guid LibraryId, Guid? BorrowerId, int Limit = 100, int Offset = 1)
        : IQuery<IList<LoanResponse>>;
}

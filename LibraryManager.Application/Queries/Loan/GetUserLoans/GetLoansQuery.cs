namespace LibraryManager.Application.Queries.Loan.GetUserLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetLoansQuery()
        : IQuery<IList<LoanResponse>>;
}

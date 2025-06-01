namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetLoansQuery(int Limit, int Offset)
        : IQuery<IList<LoanResponse>>;
}

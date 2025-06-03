namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetLoansQuery(int Limit = 100, int Offset = 1, Guid? UserId = null)
        : IQuery<IList<LoanResponse>>;
}

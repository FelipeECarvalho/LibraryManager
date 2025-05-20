namespace LibraryManager.Application.Queries.User.GetUserLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetUserLoansQuery(Guid Id) 
        : IQuery<IList<LoanResponse>>;
}

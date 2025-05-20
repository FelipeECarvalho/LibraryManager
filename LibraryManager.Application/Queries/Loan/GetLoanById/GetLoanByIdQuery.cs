namespace LibraryManager.Application.Queries.Loan.GetLoanById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetLoanByIdQuery(Guid Id) 
        : IQuery<LoanResponse>;
}

namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Loan;

    public sealed record GetLoansQuery
        : Paginable, IQuery<IList<LoanResponse>>
    {
        public Guid? BorrowerId { get; init; }
        public Guid LibraryId { get; set; }
    }
}

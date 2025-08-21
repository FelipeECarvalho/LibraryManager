namespace LibraryManager.Application.Queries.Borrower.GetBorrowers
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Models;

    public sealed record GetBorrowersQuery
        : Paginable, IQuery<IList<BorrowerResponse>>
    {
        public Guid LibraryId { get; set; }
    }
}

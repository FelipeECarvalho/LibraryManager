namespace LibraryManager.Application.Queries.Borrower.GetBorrowers
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetBorrowersQuery(int Limit = 100, int Offset = 1)
        : IQuery<IList<BorrowerResponse>>;
}

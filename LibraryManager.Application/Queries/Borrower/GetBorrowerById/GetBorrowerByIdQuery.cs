namespace LibraryManager.Application.Queries.Borrower.GetBorrowerById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetBorrowerByIdQuery(Guid Id)
        : IQuery<BorrowerResponse>;
}

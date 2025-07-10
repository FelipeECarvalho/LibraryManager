namespace LibraryManager.Application.Queries.Borrower.GetBorrowers
{
    using FluentValidation;
    using LibraryManager.Application.Validators;

    internal sealed class GetBorrowersQueryValidator
        : AbstractValidator<GetBorrowersQuery>
    {
        public GetBorrowersQueryValidator()
        {
            Include(new PaginableValidator());
        }
    }
}

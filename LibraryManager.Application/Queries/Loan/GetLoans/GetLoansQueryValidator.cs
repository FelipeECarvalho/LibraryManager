namespace LibraryManager.Application.Queries.Loan.GetLoans
{
    using FluentValidation;
    using LibraryManager.Application.Validators;

    internal sealed class GetLoansQueryValidator
        : AbstractValidator<GetLoansQuery>
    {
        public GetLoansQueryValidator()
        {
            Include(new PaginableValidator());
        }
    }
}

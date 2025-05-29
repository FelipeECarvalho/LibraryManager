namespace LibraryManager.Application.Commands.Loan.CreateLoan
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateLoanCommandValidator
        : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(DomainErrors.Loan.UserIdRequired);

            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage(DomainErrors.Loan.BookIdRequired);

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate).WithMessage(DomainErrors.Loan.BookIdRequired);

            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage(DomainErrors.Loan.BookIdRequired);
        }
    }
}

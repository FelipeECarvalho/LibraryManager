namespace LibraryManager.Application.Commands.Loan.UpdateLoanStatus
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Enums;
    using System;
    using System.Linq;

    internal sealed class UpdateLoanStatusCommandValidator
        : AbstractValidator<UpdateLoanStatusCommand>
    {
        public UpdateLoanStatusCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Status)
            .Must(status =>
                new[] { LoanStatus.Approved, LoanStatus.Returned, LoanStatus.Borrowed, LoanStatus.Cancelled }
                .Contains(status))
            .WithMessage("Invalid status value.");
        }
    }
}

namespace LibraryManager.Application.Commands.Borrower.CreateBorrower
{
    using FluentValidation;
    using LibraryManager.Application.Validators;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateBorrowerCommandValidator
        : AbstractValidator<CreateBorrowerCommand>
    {
        public CreateBorrowerCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(DomainErrors.Name.NameRequired)
                .SetValidator(new NameValidator());

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage(DomainErrors.Borrower.DocumentRequired)
                .Length(2, 30).WithMessage(DomainErrors.Borrower.DocumentInvalidLength);

            RuleFor(x => x.Address)
                .NotNull().WithMessage(DomainErrors.Address.AddressRequired)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.Password)
                .Length(8, 512).WithMessage(DomainErrors.User.PasswordInvalidLength);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(DomainErrors.Email.InvalidEmail)
                .NotEmpty().WithMessage(DomainErrors.Email.EmailRequired)
                .Length(2, 50).WithMessage(DomainErrors.Email.EmailInvalidLength);
        }
    }
}

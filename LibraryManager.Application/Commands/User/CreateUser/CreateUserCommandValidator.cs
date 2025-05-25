namespace LibraryManager.Application.Commands.User.CreateUser
{
    using FluentValidation;
    using LibraryManager.Application.Validators;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateUserCommandValidator
        : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(DomainErrors.Name.NameRequired)
                .SetValidator(new NameValidator());

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage(DomainErrors.User.DocumentRequired)
                .Length(2, 30).WithMessage(DomainErrors.User.DocumentInvalidLength);

            RuleFor(x => x.Address)
                .NotNull().WithMessage(DomainErrors.Address.AddressRequired)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(DomainErrors.User.InvalidEmail)
                .NotEmpty().WithMessage(DomainErrors.User.EmailRequired)
                .Length(2, 50).WithMessage(DomainErrors.User.EmailInvalidLength);
        }
    }
}

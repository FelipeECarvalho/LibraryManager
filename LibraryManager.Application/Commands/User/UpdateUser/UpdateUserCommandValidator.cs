namespace LibraryManager.Application.Commands.User.UpdateUser
{
    using FluentValidation;
    using LibraryManager.Application.Validators;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class UpdateUserCommandValidator
        : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(DomainErrors.Name.NameRequired)
                .SetValidator(new NameValidator());

            RuleFor(x => x.Address)
                .NotNull().WithMessage(DomainErrors.Address.AddressRequired)
                .SetValidator(new AddressValidator());
        }
    }
}

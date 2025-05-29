namespace LibraryManager.Application.Validators
{
    using FluentValidation;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.ValueObjects;

    internal sealed class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage(DomainErrors.Name.FirstNameRequired)
                .Length(2, 100).WithMessage(DomainErrors.Name.LastNameInvalidLength);

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage(DomainErrors.Name.LastNameRequired)
                .Length(2, 100).WithMessage(DomainErrors.Name.LastNameInvalidLength);
        }
    }
}

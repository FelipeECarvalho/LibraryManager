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
               .MinimumLength(2).WithMessage(DomainErrors.Name.FirstNameLengthError)
               .MaximumLength(100).WithMessage(DomainErrors.Name.FirstNameLengthError);

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage(DomainErrors.Name.LastNameRequired)
               .MinimumLength(2).WithMessage(DomainErrors.Name.LastNameLengthError)
               .MaximumLength(100).WithMessage(DomainErrors.Name.LastNameLengthError);
        }
    }
}

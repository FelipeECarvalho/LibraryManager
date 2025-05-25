namespace LibraryManager.Application.Commands.Author.UpdateAuthor
{
    using FluentValidation;
    using LibraryManager.Application.Commands.Author.CreateAuthor;
    using LibraryManager.Application.Validators;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateAuthorCommandValidator
        : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Name)
                .NotNull().WithMessage(DomainErrors.Name.NameRequired)
                .SetValidator(new NameValidator());

            RuleFor(x => x.Description)
                .MaximumLength(256).WithMessage(DomainErrors.Author.DescriptionTooLong);
        }
    }
}

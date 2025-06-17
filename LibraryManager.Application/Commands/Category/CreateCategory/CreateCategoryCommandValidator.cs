namespace LibraryManager.Application.Commands.Category.CreateCategory
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateCategoryCommandValidator
        : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.LibraryId)
                .NotEmpty().WithMessage(DomainErrors.Library.NotFound);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(DomainErrors.Category.NameRequired)
                .Length(2, 50).WithMessage(DomainErrors.Category.NameInvalidLength);

            RuleFor(x => x.Description)
                .MaximumLength(256).WithMessage(DomainErrors.Category.DescriptionInvalidLength);
        }
    }
}

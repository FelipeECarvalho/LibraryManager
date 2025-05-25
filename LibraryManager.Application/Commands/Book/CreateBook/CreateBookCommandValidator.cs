namespace LibraryManager.Application.Commands.Book.CreateBook
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class CreateBookCommandValidator
        : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(DomainErrors.Book.TitleRequired)
                .Length(2, 100).WithMessage(DomainErrors.Book.TitleInvalidLength);

            RuleFor(x => x.PublicationDate)
                .NotEmpty().WithMessage(DomainErrors.Book.PublicationDateRequired);

            RuleFor(x => x.Isbn)
                .NotEmpty().WithMessage(DomainErrors.Book.IsbnRequired)
                .Length(2, 100).WithMessage(DomainErrors.Book.IsbnInvalidLength);
        }
    }
}

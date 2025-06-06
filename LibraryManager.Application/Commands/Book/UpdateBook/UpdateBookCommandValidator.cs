﻿namespace LibraryManager.Application.Commands.Book.UpdateBook
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class UpdateBookCommandValidator
        : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(DomainErrors.Book.TitleRequired)
                .Length(2, 100).WithMessage(DomainErrors.Book.TitleInvalidLength);

            RuleFor(x => x.PublicationDate)
                .NotEmpty().WithMessage(DomainErrors.Book.PublicationDateRequired);
        }
    }
}

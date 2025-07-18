﻿namespace LibraryManager.Application.Commands.Auth.Login
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class LoginCommandValidator
        : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.LibraryId)
                .NotEmpty().WithMessage(DomainErrors.Library.NotFound);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(DomainErrors.User.PasswordRequired)
                .Length(8, 512).WithMessage(DomainErrors.User.PasswordInvalidLength);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(DomainErrors.Email.InvalidEmail)
                .NotEmpty().WithMessage(DomainErrors.Email.EmailRequired)
                .Length(2, 50).WithMessage(DomainErrors.Email.EmailInvalidLength);
        }
    }
}

namespace LibraryManager.Application.Commands.Auth.RefreshToken
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class RefreshTokenCommandValidator
        : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage(DomainErrors.General.InvalidRefreshToken);
        }
    }
}

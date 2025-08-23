namespace LibraryManager.Application.Validators
{
    using FluentValidation;
    using LibraryManager.Application.Models;
    using LibraryManager.Core.Errors;

    internal sealed class PaginableValidator
        : AbstractValidator<Paginable>
    {
        public PaginableValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage(DomainErrors.General.InvalidPageNumber);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage(DomainErrors.General.InvalidPageSize);
        }
    }
}

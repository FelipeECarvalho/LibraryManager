namespace LibraryManager.Application.Commands.Book.UpdateBookStock
{
    using FluentValidation;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;

    internal sealed class UpdateBookStockCommandValidator
        : AbstractValidator<UpdateBookStockCommand>
    {
        public UpdateBookStockCommandValidator() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage(Error.NullValue);

            RuleFor(x => x.StockNumber)
                .GreaterThanOrEqualTo(0).WithMessage(DomainErrors.Book.StockNumberInvalid);
        }
    }
}

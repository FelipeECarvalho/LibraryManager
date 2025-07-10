namespace LibraryManager.Application.Queries.Book.GetBooks
{
    using FluentValidation;
    using LibraryManager.Application.Validators;

    internal sealed class GetBooksQueryValidator
        : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            Include(new PaginableValidator());
        }
    }
}

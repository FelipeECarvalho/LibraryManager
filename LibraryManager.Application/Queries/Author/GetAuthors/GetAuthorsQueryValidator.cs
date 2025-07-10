namespace LibraryManager.Application.Queries.Author.GetAuthors
{
    using FluentValidation;
    using LibraryManager.Application.Validators;

    internal sealed class GetAuthorsQueryValidator 
        : AbstractValidator<GetAuthorsQuery>
    {
        public GetAuthorsQueryValidator()
        {
            Include(new PaginableValidator());
        }
    }
}

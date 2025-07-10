namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using FluentValidation;
    using LibraryManager.Application.Validators;

    internal sealed class GetCategoriesQueryValidator
        : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            Include(new PaginableValidator());
        }
    }
}

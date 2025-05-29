namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetCategoriesQuery
        : IQuery<CategoryResponse>;
}

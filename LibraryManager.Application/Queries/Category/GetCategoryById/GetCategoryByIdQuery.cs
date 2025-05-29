namespace LibraryManager.Application.Queries.Category.GetCategoryById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetCategoryByIdQuery(Guid Id)
        : IQuery<CategoryResponse>;
}

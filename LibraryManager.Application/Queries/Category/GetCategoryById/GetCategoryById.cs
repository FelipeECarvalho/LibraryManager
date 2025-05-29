namespace LibraryManager.Application.Queries.Category.GetCategoryById
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetCategoryById(Guid Id)
        : IQuery<CategoryResponse>;
}

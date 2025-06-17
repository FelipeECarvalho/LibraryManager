namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetCategoriesQuery(Guid libraryId, int Limit = 100, int Offset = 1)
        : IQuery<IList<CategoryResponse>>;
}

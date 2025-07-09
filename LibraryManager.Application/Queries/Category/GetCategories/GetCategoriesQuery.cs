namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record GetCategoriesQuery
        : Paginable, IQuery<IList<CategoryResponse>>
    {
        public Guid LibraryId { get; set; }
    }
}

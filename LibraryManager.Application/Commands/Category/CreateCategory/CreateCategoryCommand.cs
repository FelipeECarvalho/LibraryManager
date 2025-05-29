namespace LibraryManager.Application.Commands.Category.CreateCategory
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Category;

    public sealed record CreateCategoryCommand(
        string Name,
        string? Description) 
        : ICommand<CategoryResponse>;
}

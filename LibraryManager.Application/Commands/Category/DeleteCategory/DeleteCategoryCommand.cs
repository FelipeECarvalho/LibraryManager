namespace LibraryManager.Application.Commands.Category.DeleteCategory
{
    using LibraryManager.Application.Abstractions.Messaging;

    public sealed record DeleteCategoryCommand(Guid Id)
        : ICommand;
}

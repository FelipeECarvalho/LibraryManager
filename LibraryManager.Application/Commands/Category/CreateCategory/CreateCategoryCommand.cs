namespace LibraryManager.Application.Commands.Category.CreateCategory
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Queries.Category;
    using System.Text.Json.Serialization;

    public sealed record CreateCategoryCommand(
        string Name,
        string Description)
        : ICommand<CategoryResponse>
    {
        [JsonIgnore]
        public Guid LibraryId { get; set; }
    }
}

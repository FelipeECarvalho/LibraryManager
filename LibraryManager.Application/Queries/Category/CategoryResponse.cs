namespace LibraryManager.Application.Queries.Category
{
    public sealed record CategoryResponse
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public CategoryResponse FromEntity(Core.Entities.Category category)
        {
            return new CategoryResponse
            {
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}

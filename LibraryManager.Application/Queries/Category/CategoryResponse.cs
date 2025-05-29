namespace LibraryManager.Application.Queries.Category
{
    public sealed record CategoryResponse
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public static CategoryResponse FromEntity(Core.Entities.Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}

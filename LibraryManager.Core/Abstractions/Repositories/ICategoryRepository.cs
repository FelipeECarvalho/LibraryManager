namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAllAsync(Guid libraryId, int limit = 100, int offset = 1, CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Category category);
    }
}

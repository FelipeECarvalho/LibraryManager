namespace LibraryManager.Application.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;

    public interface ICategoryRepository
    {
        Task<IList<Category>> GetAllAsync(Guid libraryId, int pageSize = 100, int pageNumber = 1, CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Category category);
    }
}

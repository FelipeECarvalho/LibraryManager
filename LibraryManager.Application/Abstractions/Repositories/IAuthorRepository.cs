namespace LibraryManager.Application.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(int pageSize = 100, int pageNumber = 1, CancellationToken cancellationToken = default);
        Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Author author);
    }
}

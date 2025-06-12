namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(int limit = 100, int offset = 1, CancellationToken cancellationToken = default);
        Task<Author> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Author author);
    }
}

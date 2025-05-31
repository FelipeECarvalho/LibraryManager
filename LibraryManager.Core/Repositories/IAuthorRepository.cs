namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.ValueObjects.Filters;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(AuthorFilter filter, CancellationToken ct = default);
        Task<Author> GetByIdAsync(Guid id, CancellationToken ct = default);

        void Add(Author author);
        void Update(Author author);
    }
}

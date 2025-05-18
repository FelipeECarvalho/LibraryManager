namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(CancellationToken ct);
        Task<Author> GetByIdAsync(Guid id, CancellationToken ct);

        void Add(Author author);
        void Update(Author author);
    }
}

namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync(CancellationToken ct);
        Task<Author> GetByIdAsync(Guid id);

        void Add(Author author);
        void Update(Author author);
    }
}

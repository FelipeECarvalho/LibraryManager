namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync(CancellationToken ct = default);
        Task<Book> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken ct = default);
        Task<IList<Book>> GetByTitleAsync(string title, CancellationToken ct = default);

        void Add(Book book);
        void Update(Book book);
    }
}

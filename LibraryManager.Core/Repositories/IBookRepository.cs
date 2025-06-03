namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync(int limit = 100, int offset = 1, string title = null, CancellationToken ct = default);
        Task<Book> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken ct = default);
        Task<bool> IsIsbnUnique(string isbn, CancellationToken ct = default);

        void Add(Book book);
        void Update(Book book);
    }
}

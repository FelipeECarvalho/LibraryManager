namespace LibraryManager.Core.Interfaces.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync(int limit = 100, int offset = 1, string title = null, CancellationToken cancellationToken = default);
        Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken cancellationToken = default);
        Task<bool> IsIsbnUnique(string isbn, CancellationToken cancellationToken = default);

        void Add(Book book);
        void Update(Book book);
    }
}

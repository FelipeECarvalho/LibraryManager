namespace LibraryManager.Core.Abstractions.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync(Guid libraryId, int limit = 100, int offset = 1, string title = null, CancellationToken cancellationToken = default);
        Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<Book>> GetByIdAsync(IList<Guid> ids, CancellationToken cancellationToken = default);
        Task<bool> IsIsbnUnique(string isbn, Guid libraryId, CancellationToken cancellationToken = default);

        void Add(Book book);
    }
}

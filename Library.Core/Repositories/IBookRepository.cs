namespace Library.Core.Repositories
{
    using Library.Core.Entities;

    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(Guid id);
        Task<IList<Book>> GetByTitleAsync(string title);

        void Add(Book book);
        void Update(Book book);
    }
}

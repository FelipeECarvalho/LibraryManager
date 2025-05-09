using Library.Core.Entities;

namespace Library.Core.Repositories
{
    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<IList<Book>> GetByTitleAsync(string title);

        void Add(Book book);
        void Update(Book book);
    }
}

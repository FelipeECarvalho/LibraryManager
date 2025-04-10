using Library.Core.Entities;

namespace Library.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);

        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
    }
}

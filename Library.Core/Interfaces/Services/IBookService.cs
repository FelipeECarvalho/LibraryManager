using Library.Core.Entities;

namespace Library.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<IList<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);

        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}

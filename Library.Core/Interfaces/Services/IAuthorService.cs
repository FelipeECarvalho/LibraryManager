using Library.Core.Entities;

namespace Library.Core.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);

        Task UpdateAsync(Author author);
        Task AddBookAsync(Author author, int bookId);
        Task DeleteAsync(Author author);
    }
}

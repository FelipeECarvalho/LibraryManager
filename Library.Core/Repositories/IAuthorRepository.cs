using Library.Core.Entities;

namespace Library.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);

        Task CreateAsync(Author author);
        Task UpdateAsync(Author author);
    }
}

using Library.Core.Entities;

namespace Library.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);

        void Add(Author author);
        void Update(Author author);
    }
}

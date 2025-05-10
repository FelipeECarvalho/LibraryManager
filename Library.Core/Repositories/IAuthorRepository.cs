namespace Library.Core.Repositories
{
    using Library.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);

        void Add(Author author);
        void Update(Author author);
    }
}

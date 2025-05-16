namespace LibraryManager.Core.Repositories
{
    using LibraryManager.Core.Entities;

    public interface IAuthorRepository
    {
        Task<IList<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(Guid id);

        void Add(Author author);
        void Update(Author author);
    }
}

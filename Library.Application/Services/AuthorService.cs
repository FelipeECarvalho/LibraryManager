using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class AuthorService(IAuthorRepository _repository) : IAuthorService
    {
        public async Task<Author> CreateAsync(Author author)
        {
            await _repository.CreateAsync(author);
            return author;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id);
            author.IsDeleted = true;
            author.UpdateDate = DateTime.Now;

            await _repository.UpdateAsync(author);
        }

        public async Task<IList<Author>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Author author)
        {
            await _repository.UpdateAsync(author);
        }
    }
}

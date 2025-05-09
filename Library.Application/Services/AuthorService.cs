using Library.Core.Entities;
using Library.Core.Interfaces.Services;
using Library.Core.Repositories;

namespace Library.Application.Services
{
    public sealed class AuthorService(IAuthorRepository _repository, IBookRepository bookRepository) : IAuthorService
    {
        public async Task<Author> CreateAsync(Author author)
        {
            await _repository.CreateAsync(author);
            return author;
        }

        public async Task<IList<Author>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(Author author)
        {
            author.Delete();
            await _repository.UpdateAsync(author);
        }

        public async Task UpdateAsync(Author author)
        {
            await _repository.UpdateAsync(author);
        }

        public async Task AddBookAsync(Author author, int bookId)
        {
            var book = await bookRepository.GetByIdAsync(bookId) 
                ?? throw new ArgumentException("Book not found");

            author.AddBook(book);

            await _repository.UpdateAsync(author);
        }
    }
}

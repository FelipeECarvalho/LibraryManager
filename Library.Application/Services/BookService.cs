using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public sealed class BookService(IBookRepository _repository) : IBookService
    {
        public async Task<IList<Book>> GetAllAsync() 
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IList<Book>> GetByTitleAsync(string title)
        {
            return await _repository.GetByTitleAsync(title);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _repository.CreateAsync(book);
            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            await _repository.UpdateAsync(book);
        }

        public async Task DeleteAsync(Book book)
        {
            book.Delete();
            await _repository.UpdateAsync(book);
        }
    }
}

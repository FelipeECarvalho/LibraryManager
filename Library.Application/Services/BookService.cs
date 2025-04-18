using Library.Core.Entities;
using Library.Core.Interfaces.Repositories;
using Library.Core.Interfaces.Services;

namespace Library.Application.Services
{
    public class BookService(IBookRepository _repository) : IBookService
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

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            book.IsDeleted = true;
            book.UpdateDate = DateTime.Now;

            await _repository.UpdateAsync(book);
        }
    }
}

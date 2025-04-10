using Library.Application.InputModels.Books;
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

        public async Task CreateAsync(BookCreateInputModel model)
        {
            var book = new Book();
            await _repository.CreateAsync(book);
        }

        public async Task UpdateAsync(BookUpdateInputModel model)
        {
            var book = new Book();
            await _repository.UpdateAsync(book);
        }
    }
}

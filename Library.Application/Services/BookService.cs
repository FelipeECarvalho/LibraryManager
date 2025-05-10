using Library.Core.Entities;
using Library.Core.Repositories;

namespace Library.Application.Services
{
    public sealed class BookService(IBookRepository _repository, IUnitOfWork _unityOfWork)
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
            _repository.Add(book);
            await _unityOfWork.SaveChangesAsync();

            return book;
        }

        public async Task UpdateAsync(Book book)
        {
            _repository.Update(book);

            await _unityOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            book.SetDeleted();
            _repository.Update(book);

            await _unityOfWork.SaveChangesAsync();
        }
    }
}

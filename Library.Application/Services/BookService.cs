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

        public async Task<Book> CreateAsync(BookCreateInputModel model)
        {
            var book = new Book
            {
                ISBN = model.ISBN,
                Title = model.Title,
                AuthorId = model.AuthorId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Description = model.Description,
                PublicationDate = model.PublicationDate,
            };

            await _repository.CreateAsync(book);
            return book;
        }

        public async Task UpdateAsync(int id, BookUpdateInputModel model)
        {
            var book = await GetByIdAsync(id);

            book.Title = model.Title;
            book.UpdateDate = DateTime.Now;
            book.Description = model.Description;
            book.PublicationDate = model.PublicationDate;

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

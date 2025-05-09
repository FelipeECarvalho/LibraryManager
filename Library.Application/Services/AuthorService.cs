using Library.Core.Entities;
using Library.Core.Repositories;

namespace Library.Application.Services
{
    public sealed class AuthorService(IAuthorRepository _repository, IBookRepository _bookRepository, IUnitOfWork _unitOfWork)
    {
        public async Task<IList<Author>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            _repository.Add(author);
            await _unitOfWork.SaveChangesAsync();

            return author;
        }

        public async Task DeleteAsync(Author author)
        {
            author.Delete();
            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _repository.Update(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddBookAsync(Author author, int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId) 
                ?? throw new ArgumentException("Book not found");

            author.AddBook(book);

            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

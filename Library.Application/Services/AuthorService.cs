namespace Library.Application.Services
{
    using Library.Core.Entities;
    using Library.Core.Repositories;
    using static System.Reflection.Metadata.BlobBuilder;

    public sealed class AuthorService(IAuthorRepository _repository, IBookRepository _bookRepository, IUnitOfWork _unitOfWork)
    {
        public async Task<IList<Author>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id)
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
            author.SetDeleted();
            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _repository.Update(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddBookAsync(Author author, IList<Guid> bookIds)
        {
            var books = await _bookRepository.GetByIdAsync(bookIds);

            ValidateAddBooks(books, bookIds);

            author.AddBook(books);

            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();
        }

        private static void ValidateAddBooks(IList<Book> books, IList<Guid> bookIds)
        {
            if (books == null || !books.Any())
            {
                throw new ArgumentException("books were not found");
            }

            var booksNotFound = bookIds.Except(books.Select(x => x.Id));
            if (booksNotFound.Any())
            {
                throw new ArgumentException($"The following book IDs were not found in the system: {string.Join(',', booksNotFound)}");
            }
        }
    }
}

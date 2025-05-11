namespace Library.Application.Services
{
    using Library.Core.Entities;
    using Library.Core.Repositories;

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

            if (books == null || !books.Any())
            {
                throw new ArgumentException("books not found");
            }

            foreach (var book in books)
            { 
                if (!bookIds.Contains(book.Id))
                {
                    throw new ArgumentException($"The book with ID: {book.Id} was not found");
                } 

                author.AddBook(book);
            }

            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

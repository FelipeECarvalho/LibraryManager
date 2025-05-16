namespace LibraryManager.Application.Services
{
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;

    public sealed class AuthorService(IAuthorRepository _repository, IBookRepository _bookRepository, IUnitOfWork _unitOfWork)
    {
        public async Task<Result<IList<Author>>> GetAllAsync()
        {
            return Result.Success(await _repository.GetAllAsync());
        }

        public async Task<Result<Author>> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Result<Author>> CreateAsync(Author author)
        {
            _repository.Add(author);
            await _unitOfWork.SaveChangesAsync();

            return author;
        }

        public async Task<Result> DeleteAsync(Author author)
        {
            author.SetDeleted();
            _repository.Update(author);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(Author author)
        {
            _repository.Update(author);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> AddBookAsync(Author author, IList<Guid> bookIds)
        {
            var books = await _bookRepository.GetByIdAsync(bookIds);

            var validationResult = ValidateAddBooks(books, bookIds);

            if (!validationResult.IsSuccess)
            {
                return Result.Failure(validationResult.Error);
            }

            author.AddBook(books);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        private static Result ValidateAddBooks(IList<Book> books, IList<Guid> bookIds)
        {
            if (books == null || !books.Any())
            {
                return Result.Failure(Error.ProvidedBooksNotFound);
            }

            var existingIds = books.Select(x => x.Id).ToHashSet();
            var booksNotFound = bookIds.Where(id => !existingIds.Contains(id));

            if (booksNotFound.Any())
            {
                return Result.Failure(Error.ProvidedBooksNotFound);
            }

            return Result.Success();
        }
    }
}

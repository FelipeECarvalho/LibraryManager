namespace LibraryManager.Application.Services
{
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;

    public sealed class BookService(IBookRepository _repository, IUnitOfWork _unitOfWork)
    {
        public async Task UpdateAsync(Book book)
        {
            _repository.Update(book);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            book.SetDeleted();
            _repository.Update(book);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

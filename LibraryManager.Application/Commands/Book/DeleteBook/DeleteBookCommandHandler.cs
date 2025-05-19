namespace LibraryManager.Application.Commands.Book.DeleteBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DeleteBookCommandHandler
        : ICommandHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken ct)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, ct);

            if (book == null)
            {
                return Result.Failure(DomainErrors.Book.NotFound(request.Id));
            }

            book.SetDeleted();
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}

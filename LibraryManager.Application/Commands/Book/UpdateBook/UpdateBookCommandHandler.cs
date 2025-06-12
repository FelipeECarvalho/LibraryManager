namespace LibraryManager.Application.Commands.Book.UpdateBook
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateBookCommandHandler
        : ICommandHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(
            IUnitOfWork unitOfWork,
            IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
            {
                return Result.Failure(DomainErrors.Book.NotFound(request.Id));
            }

            book.Update(request.Title, request.Description, request.PublicationDate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

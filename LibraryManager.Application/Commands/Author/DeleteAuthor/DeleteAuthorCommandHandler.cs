namespace LibraryManager.Application.Commands.Author.DeleteAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class DeleteAuthorCommandHandler
        : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken ct)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id, ct);

            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.NotFound(request.Id));
            }

            author.SetDeleted();
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}

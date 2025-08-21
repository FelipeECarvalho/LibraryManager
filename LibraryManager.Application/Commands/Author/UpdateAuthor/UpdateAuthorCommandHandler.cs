namespace LibraryManager.Application.Commands.Author.UpdateAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UpdateAuthorCommandHandler
        : ICommandHandler<UpdateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (author == null)
            {
                return Result.Failure(DomainErrors.Author.NotFound(request.Id));
            }

            author.Update(request.Name, request.Description);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

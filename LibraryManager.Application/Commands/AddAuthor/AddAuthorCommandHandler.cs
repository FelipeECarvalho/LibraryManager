namespace LibraryManager.Application.Commands.AddAuthor
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using System.Threading.Tasks;

    public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author(request.Name, request.Description);

            _unitOfWork.Authors.Add(author);
            
            await _unitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return Result.Success();
        }
    }
}

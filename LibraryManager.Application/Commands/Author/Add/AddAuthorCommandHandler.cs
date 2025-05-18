namespace LibraryManager.Application.Commands.Author.Add
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using LibraryManager.Core.Repositories;
    using System.Threading.Tasks;

    internal class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        internal AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddAuthorCommand request, CancellationToken ct)
        {
            var author = new Author(request.Name, request.Description);

            _unitOfWork.Authors.Add(author);
            
            await _unitOfWork
                .SaveChangesAsync(ct)
                .ConfigureAwait(false);

            return Result.Success();
        }
    }
}

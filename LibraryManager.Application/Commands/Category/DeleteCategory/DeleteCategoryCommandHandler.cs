namespace LibraryManager.Application.Commands.Category.DeleteCategory
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DeleteCategoryCommandHandler
        : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository)
        {
            _unityOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await _categoryRepository.GetById(request.Id, ct);

            if (category == null)
            {
                return Result.Failure(DomainErrors.Category.NotFound(request.Id));
            }

            category.SetDeleted();
            await _unityOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}

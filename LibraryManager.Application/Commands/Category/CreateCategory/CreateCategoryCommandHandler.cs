namespace LibraryManager.Application.Commands.Category.CreateCategory
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Application.Abstractions.Repositories;
    using LibraryManager.Application.Queries.Category;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CreateCategoryCommandHandler
        : ICommandHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name, request.Description, request.LibraryId);

            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CategoryResponse.FromEntity(category);
        }
    }
}

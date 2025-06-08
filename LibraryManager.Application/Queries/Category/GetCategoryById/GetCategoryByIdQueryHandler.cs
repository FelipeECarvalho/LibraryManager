namespace LibraryManager.Application.Queries.Category.GetCategoryById
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Errors;
    using LibraryManager.Core.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetCategoryByIdQueryHandler
        : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (category == null)
            {
                return Result.Failure<CategoryResponse>(DomainErrors.Category.NotFound(request.Id));
            }

            return CategoryResponse.FromEntity(category);
        }
    }
}

namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Abstractions.Repositories;
    using LibraryManager.Core.Common;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetCategoriesQueryHandler
        : IQueryHandler<GetCategoriesQuery, IList<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IList<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(
                request.LibraryId,
                request.PageSize,
                request.PageNumber,
                cancellationToken);

            var response = categories?
                .Select(CategoryResponse.FromEntity)?
                .ToList();

            return response;
        }
    }
}

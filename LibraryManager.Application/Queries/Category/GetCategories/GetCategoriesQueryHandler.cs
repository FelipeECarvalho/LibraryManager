namespace LibraryManager.Application.Queries.Category.GetCategories
{
    using LibraryManager.Application.Abstractions.Messaging;
    using LibraryManager.Core.Common;
    using LibraryManager.Core.Repositories;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetCategoriesQueryHandler
        : IQueryHandler<GetCategoriesQuery, IList<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IList<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync(
                request.Limit,
                request.Offset,
                ct);

            var response = categories?
                .Select(CategoryResponse.FromEntity)?
                .ToList();

            return response;
        }
    }
}

namespace LibraryManager.API.Controllers
{
    using LibraryManager.Application.Commands.Category.CreateCategory;
    using LibraryManager.Application.Commands.Category.DeleteCategory;
    using LibraryManager.Application.Queries.Category;
    using LibraryManager.Application.Queries.Category.GetCategories;
    using LibraryManager.Application.Queries.Category.GetCategoryById;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Hybrid;

    /// <summary>
    /// A category
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Operator,Admin")]
    public class CategoriesController(
        IMediator _mediator,
        HybridCache _hybridCache) : ApiControllerBase
    {
        private readonly string _categoriesCacheTag = "categories";

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <response code="200">Categories retrieved successfully.</response>
        /// <returns>A list containing all categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<CategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetCategoriesQuery query,
            CancellationToken cancellationToken)
        {
            query.LibraryId = LibraryId;
            var cacheKey = $"library:{query.LibraryId}:categories";

            var result = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async _ => await _mediator.Send(query, cancellationToken),
                tags: [_categoriesCacheTag],
                cancellationToken: cancellationToken);

            var categories = result.Value;
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves the category with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <response code="200">Category retrieved successfully.</response>
        /// <response code="404">Category not found.</response>
        /// <returns>Returns the category if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetCategoryByIdQuery(id);
            var cacheKey = $"category:{id}";

            var result = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async _ => await _mediator.Send(query, cancellationToken),
                tags: [_categoriesCacheTag],
                cancellationToken: cancellationToken);

            if (result.IsFailure)
            {
                return HandleFailure(result);
            }

            var category = result.Value;
            return Ok(category);
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="categoryRequest">An object containing the category's data.</param>
        /// <response code="201">Category created successfully.</response>
        /// <response code="400">Validation error.</response>
        /// <returns>The newly created category.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            CreateCategoryCommand categoryRequest,
            CancellationToken cancellationToken)
        {
            categoryRequest.LibraryId = LibraryId;
            var response = await _mediator.Send(categoryRequest, cancellationToken);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            var category = response.Value;
            return Ok(category);
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <response code="204">Category deleted successfully.</response>
        /// <response code="404">Category not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            await _hybridCache.RemoveAsync(_categoriesCacheTag, cancellationToken);

            return NoContent();
        }
    }
}

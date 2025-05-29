namespace LibraryManager.API.Controllers
{
    using Asp.Versioning;
    using LibraryManager.Application.Commands.Category.CreateCategory;
    using LibraryManager.Application.Commands.Category.DeleteCategory;
    using LibraryManager.Application.Queries.Category;
    using LibraryManager.Application.Queries.Category.GetCategories;
    using LibraryManager.Application.Queries.Category.GetCategoryById;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A category
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ApiControllerBase
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <response code="200">Categories retrieved successfully.</response>
        /// <returns>A list containing all categories.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<CategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken ct)
        {
            var response = await _mediator.Send(new GetCategoriesQuery(), ct);

            return Ok(response);
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
            CancellationToken ct)
        {
            var response = await _mediator.Send(new GetCategoryById(id), ct);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            return Ok(response);
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
            CancellationToken ct)
        {
            var response = await _mediator.Send(categoryRequest, ct);

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
            CancellationToken ct)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(id), ct);

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }

            return NoContent();
        }
    }
}

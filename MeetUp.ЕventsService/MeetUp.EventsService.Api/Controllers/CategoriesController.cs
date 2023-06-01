using MeetUp.EventsService.Api.Features;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.EventsService.Api.Controllers
{

    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryManager;

        public CategoriesController(ICategoryService categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> GetAllCategoriesAsync(
            [FromQuery] CategoryQueryDto categoryQuery,
            CancellationToken cancellationToken)
        {
            var categoeies = await _categoryManager.GetAllCategoriesAsync(categoryQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputCategoryDto>>(categoeies);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _categoryManager.GetCategoryByIdAsync(categoryId, cancellationToken);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _categoryManager.CreateCategoryAsync(categoryDto, cancellationToken);

            return Created(nameof(CreateCategoryAsync), categoryId);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken)
        {
            await _categoryManager.DeleteCategoryByIdAsync(categoryId, cancellationToken);

            return NoContent();
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync(
            [FromRoute] Guid categoryId,
            [FromBody] CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            var updatingCategoryId = await _categoryManager.UpdateCategoryByIdAsync(categoryId, categoryDto, cancellationToken);

            return Ok(updatingCategoryId);
        }
    }
}

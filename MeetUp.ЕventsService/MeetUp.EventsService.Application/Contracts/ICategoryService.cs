using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;

namespace MeetUp.EventsService.Application.Contracts
{
    public interface ICategoryService
    {
        Task<PagedList<OutputCategoryDto>> GetAllCategoriesAsync(
            CategoryQueryDto categoryQuery,
            CancellationToken cancellationToken);

        Task<OutputCategoryDto> GetCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken);

        Task<Guid> CreateCategoryAsync(
            CategoryDto categoryDto,
            CancellationToken cancellationToken);

        Task DeleteCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken);

        Task<Guid> UpdateCategoryByIdAsync(
            Guid categoryId,
            CategoryDto categoryDto,
            CancellationToken cancellationToken);
    }
}

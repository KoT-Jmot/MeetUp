using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;

namespace MeetUp.EventsService.Application.Contracts
{
    public interface ICategoryService
    {
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

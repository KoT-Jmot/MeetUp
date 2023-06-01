using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.Utils.Excaption;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using FluentValidation;
using Mapster;

namespace MeetUp.EventsService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IValidator<CategoryDto> _categoryValidator;

        public CategoryService(
            IRepositoryManager repositoryManager,
            IValidator<CategoryDto> categoryValidator)
        {
            _repositoryManager = repositoryManager;
            _categoryValidator = categoryValidator;
        }

        public async Task<Guid> CreateCategoryAsync(
            CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            await _categoryValidator.ValidateAndThrowAsync(categoryDto, cancellationToken);

            var existedCategory = await _repositoryManager.Categories.GetCategoryByNameAsync(categoryDto.Name!, trackChanges: false, cancellationToken);

            if (existedCategory is not null)
                throw new CreatingCategoryException("This category already exists!");

            var category = categoryDto.Adapt<Category>();

            await _repositoryManager.Categories.AddAsync(category, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return category.Id;
        }

        public async Task DeleteCategoryByIdAsync(
            Guid categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _repositoryManager.Categories.GetByIdAsync(categoryId, trackChanges: false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            await _repositoryManager.Categories.RemoveAsync(category, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> UpdateCategoryByIdAsync(
            Guid categoryId,
            CategoryDto categoryDto,
            CancellationToken cancellationToken)
        {
            await _categoryValidator.ValidateAndThrowAsync(categoryDto, cancellationToken);

            var existedCategory = await _repositoryManager.Categories.GetCategoryByNameAsync(categoryDto.Name!, trackChanges: false, cancellationToken);

            if (existedCategory is not null)
                throw new CreatingCategoryException("This category already exists!");

            var updatingCategory = await _repositoryManager.Categories.GetByIdAsync(categoryId, trackChanges: true, cancellationToken);

            if (updatingCategory is null)
                throw new EntityNotFoundException("Catgory was not found!");

            updatingCategory = categoryDto.Adapt<Category>();

            return categoryId;
        }
    }
}

using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.Services;
using FluentValidation;
using Moq;
using MeetUp.EventsService.Infrastructure.Models;
using MeetUp.EventsService.Application.Utils.Excaption;

namespace MeetUp.EventsService.Tests.UnitTests.ServicesTests.CatigoryServiceTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManager;
        private readonly Mock<IValidator<CategoryDto>> _categoryValidator;

        private readonly ICategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryValidator = new Mock<IValidator<CategoryDto>>();
            _repositoryManager = RepositoryManagerMock.CreateForCategoryTests();

            _categoryService = new CategoryService(_repositoryManager.Object, _categoryValidator.Object);
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryExists_ShouldReturnOutputCategoryDto()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryEntity();
            var categoryId = categoryDto.Id;

            //Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId, CancellationToken.None);

            //Assert
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryDoesNotExist_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var categoryId = Guid.NewGuid();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               categoryId,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Category));

            //Act
            var getCategoryByIdAsyncProcess = _categoryService.GetCategoryByIdAsync(categoryId, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await getCategoryByIdAsyncProcess);
        }

        [Fact]
        public async Task GetAllCategories_WithoutFiltration_ShouldReturnListOfOutputCategoryDto()
        {
            //Arrange
            var categoryQuery = new CategoryQueryDto();

            //Act
            var result = await _categoryService.GetAllCategoriesAsync(categoryQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), result.MetaData.TotalCount);
        }

        [Fact]
        public async Task GetAllCategories_WithFiltration_ShouldReturnListOfOutputCategoryDto()
        {
            //Arrange
            var categoryEntity = CategoryDataFactory.GetAllCategoryEntity().First();

            var categoryQuery = new CategoryQueryDto()
            {
                Name = categoryEntity.Name,
            };

            //Act
            var result = await _categoryService.GetAllCategoriesAsync(categoryQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), result.MetaData.TotalCount);
        }

        [Fact]
        public async Task CreateCategory_WhenCategoryDoNotExist_ShouldReturnCreatedCategoryId()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();

            //Act
            var result = await _categoryService.CreateCategoryAsync(categoryDto, CancellationToken.None);

            //Assert
            _repositoryManager.Verify(lw=>lw.Categories.AddAsync(It.IsAny<Category>(),CancellationToken.None));
        }

        [Fact]
        public async Task CreateCategory_WhenCategoryExist_ShouldReturnCreatingCategoryException()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();

            _repositoryManager.Setup(r => r.Categories.GetCategoryByNameAsync(
                               CategoryDataFactory.GetCategoryEntity().Name,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(CategoryDataFactory.GetCategoryEntity());

            //Act
            var createCategoryAsyncProcess = _categoryService.CreateCategoryAsync(categoryDto, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<CreatingCategoryException>(async () => await createCategoryAsyncProcess);
        }

        [Fact]
        public async Task DeleteCategory_WhenCategoryExist_ShouldReturnSuccessResult()
        {
            //Arrange
            var categoryId = CategoryDataFactory.GetCategoryEntity().Id;

            //Act
            await _categoryService.DeleteCategoryByIdAsync(categoryId, CancellationToken.None);

            //Assert
            _repositoryManager.Verify(lw=>lw.Categories.RemoveAsync(
                               It.IsAny<Category>(),
                               CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCategory_WhenCategoryDoNotExist_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var categoryId = Guid.NewGuid();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               categoryId,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Category));

            //Act
            var deleteCategoryByIdAsyncProcess = _categoryService.DeleteCategoryByIdAsync(categoryId, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await deleteCategoryByIdAsyncProcess);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryExistWithCorrectData_ShouldReturnUpdatedCategoryId()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();
            var categoryId = CategoryDataFactory.GetCategoryEntity().Id;

            //Act
            var result = await _categoryService.UpdateCategoryByIdAsync(categoryId, categoryDto, CancellationToken.None);

            //Assert
            Assert.Equal(categoryId, result);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryExistWithIncorrectData_ShouldReturnCreatingCategoryException()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();
            var categoryId = CategoryDataFactory.GetCategoryEntity().Id;

            _repositoryManager.Setup(r => r.Categories.GetCategoryByNameAsync(
                               categoryDto.Name!,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(CategoryDataFactory.GetCategoryEntity());

            //Act
            var pdateCategoryByIdAsyncProcess = _categoryService.UpdateCategoryByIdAsync(categoryId, categoryDto, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<CreatingCategoryException>(async () => await pdateCategoryByIdAsyncProcess);
        }

        [Fact]
        public async Task UpdateCategory_WhenCategoryIsNotExist_ShouldReturnCreatingCategoryException()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();
            var categoryId = Guid.NewGuid();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               categoryId,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Category));

            //Act
            var pdateCategoryByIdAsyncProcess = _categoryService.UpdateCategoryByIdAsync(categoryId, categoryDto, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await pdateCategoryByIdAsyncProcess);
        }
    }
}

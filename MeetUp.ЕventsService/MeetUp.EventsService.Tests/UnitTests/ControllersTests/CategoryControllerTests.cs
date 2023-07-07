using MeetUp.EventsService.Api.Controllers;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ControllersTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly CategoriesController _categoriesController;

        public CategoryControllerTests()
        {
            _categoryService = MockConfigure.CreateCategoryServiceMock();
            _categoriesController = new CategoriesController(_categoryService.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var categoryQuery = new CategoryQueryDto();

            //Act
            var result = await _categoriesController.GetAllCategoriesAsync(categoryQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _categoryService.Verify(r => r.GetAllCategoriesAsync(categoryQuery, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GetCategoryByIdAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var categoryId = CategoryDataFactory.GetCategoryEntity().Id;

            //Act
            var result = await _categoriesController.GetCategoryByIdAsync(categoryId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _categoryService.Verify(r => r.GetCategoryByIdAsync(categoryId, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task CreateCategoryAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var categoryDto = CategoryDataFactory.GetCategoryDto();

            //Act
            var result = await _categoriesController.CreateCategoryAsync(categoryDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _categoryService.Verify(r => r.CreateCategoryAsync(categoryDto, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteCategoryAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var categoryId = Guid.NewGuid();

            //Act
            await _categoriesController.DeleteCategoryAsync(categoryId, CancellationToken.None);

            //Assert
            _categoryService.Verify(r => r.DeleteCategoryByIdAsync(categoryId, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task UpdateCategoryAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryDto = CategoryDataFactory.GetCategoryDto();

            //Act
            var result = await _categoriesController.UpdateCategoryAsync(categoryId, categoryDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _categoryService.Verify(r => r.UpdateCategoryByIdAsync(It.IsAny<Guid>(), categoryDto, It.IsAny<CancellationToken>()));
        }
    }
}

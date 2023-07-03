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
            _repositoryManager = RepositoryManagerMock.Create();

            _categoryService = new CategoryService(_repositoryManager.Object, _categoryValidator.Object);
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var user = DataFactory.GetCategoryEntity();
            var userId = user.Id;

            //Act
            var result = await _categoryService.GetCategoryByIdAsync(userId, CancellationToken.None);

            //Assert
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetCategoryById_WhenCategoryDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var userId = Guid.NewGuid();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                       userId,
                       It.IsAny<bool>(),
                       It.IsAny<CancellationToken>()))
                       .ReturnsAsync(default(Category));

            //Act
            var getCategoryByIdAsyncProcess = _categoryService.GetCategoryByIdAsync(userId, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await getCategoryByIdAsyncProcess);
        }

        [Fact]
        public async Task GetAllCategories_WithoutFiltration_ShouldReturnSuccessResult()
        {
            //Arrange
            var categoryQuery = new CategoryQueryDto();
            var usersCount = DataFactory.GetAllCategoryEntity().Count();

            //Act
            var result = await _categoryService.GetAllCategoriesAsync(categoryQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(usersCount, result.Count());
        }

        [Fact]
        public async Task GetAllCategories_WithFiltration_ShouldReturnSuccessResult()
        {
            //Arrange
            var users = DataFactory.GetAllCategoryEntity();
            var categoryQuery = new CategoryQueryDto();
            categoryQuery.Name = users.First().Name;

            //Act
            var result = await _categoryService.GetAllCategoriesAsync(categoryQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotEqual(users.Count(), result.Count());
        }
    }
}

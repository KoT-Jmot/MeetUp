using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ControllersTests
{
    public static class CategoryServiceMock
    {
        public static Mock<ICategoryService> Create()
        { 
            var categoryService = new Mock<ICategoryService>();

            categoryService.Setup(r => r.GetAllCategoriesAsync(
                            It.IsAny<CategoryQueryDto>(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(DataFactory.GetAllOutputCategoriesDto());

            categoryService.Setup(r => r.GetCategoryByIdAsync(
                            DataFactory.GetCategoryEntity().Id,
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(DataFactory.GetOutputCategoryDto());

            categoryService.Setup(r => r.CreateCategoryAsync(
                            DataFactory.GetCategoryDto(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(DataFactory.GetCategoryEntity().Id);

            categoryService.Setup(r => r.DeleteCategoryByIdAsync(
                            It.IsAny<Guid>(),
                            It.IsAny<CancellationToken>()));

            categoryService.Setup(r => r.UpdateCategoryByIdAsync(
                            It.IsAny<Guid>(),
                            DataFactory.GetCategoryDto(),
                            It.IsAny<CancellationToken>()))
                           .ReturnsAsync(Guid.NewGuid());

            return categoryService;
        }
    }
}

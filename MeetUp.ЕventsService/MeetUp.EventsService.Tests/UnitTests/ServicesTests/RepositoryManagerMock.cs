using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using MockQueryable.Moq;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ServicesTests
{
    public static class RepositoryManagerMock
    {
        public static Mock<IRepositoryManager> Create()
        {
            var repositoryManager = new Mock<IRepositoryManager>();

            repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               DataFactory.GetCategoryEntity().Id,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(DataFactory.GetCategoryEntity());

            repositoryManager.Setup(r => r.Categories.GetAll(It.IsAny<bool>()))
                             .Returns(DataFactory.GetAllCategoryEntity().BuildMock());

            repositoryManager.Setup(r => r.Categories.AddAsync(
                              DataFactory.GetCategoryEntity(),
                              It.IsAny<CancellationToken>()));

            repositoryManager.Setup(r => r.Categories.RemoveAsync(
                              DataFactory.GetCategoryEntity(),
                              It.IsAny<CancellationToken>()));

            repositoryManager.Setup(r => r.Categories.GetCategoryByNameAsync(
                              DataFactory.GetCategoryEntity().Name,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(default(Category));

            repositoryManager.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));

            return repositoryManager;
        }
    }
}

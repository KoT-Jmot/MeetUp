using MeetUp.EventsService.Infrastructure.Contracts;
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

            return repositoryManager;
        }
    }
}

using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using MockQueryable.Moq;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ServicesTests
{
    public static class RepositoryManagerMock
    {
        public static Mock<IRepositoryManager> CreateForCategoryTests()
        {
            var repositoryManager = new Mock<IRepositoryManager>();

            repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                              CategoryDataFactory.GetCategoryEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(CategoryDataFactory.GetCategoryEntity());

            repositoryManager.Setup(r => r.Categories.GetAll(It.IsAny<bool>()))
                             .Returns(CategoryDataFactory.GetAllCategoryEntity().BuildMock());

            repositoryManager.Setup(r => r.Categories.GetCategoryByNameAsync(
                              CategoryDataFactory.GetCategoryEntity().Name,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(default(Category));

            return repositoryManager;
        }

        public static Mock<IRepositoryManager> CreateForEventTests()
        {
            var repositoryManager = new Mock<IRepositoryManager>();

            repositoryManager.Setup(r=>r.Events.GetByIdAsync(
                              EventDataFactory.GetEventEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(EventDataFactory.GetEventEntity());

            repositoryManager.Setup(r => r.Events.GetAll((It.IsAny<bool>())))
                             .Returns(EventDataFactory.GetEvents().BuildMock());

            repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                              CategoryDataFactory.GetCategoryEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(CategoryDataFactory.GetCategoryEntity());

            repositoryManager.Setup(r => r.Events.GetEventByIdAndUserIdAsync(
                              EventDataFactory.GetEventEntity().Id,
                              EventDataFactory.GetEventEntity().SponsorId,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(EventDataFactory.GetEventEntity());

            return repositoryManager;
        }
    }
}

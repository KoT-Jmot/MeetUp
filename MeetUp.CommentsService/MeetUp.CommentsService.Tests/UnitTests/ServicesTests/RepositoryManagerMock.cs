using MeetUp.CommentsService.Infrastructure.Contracts;
using MockQueryable.Moq;
using Moq;

namespace MeetUp.CommentsService.Tests.UnitTests.ServicesTests
{
    public class RepositoryManagerMock
    {
        public static Mock<IRepositoryManager> Create()
        {
            var repositoryManager = new Mock<IRepositoryManager>();

            repositoryManager.Setup(r => r.Comments.GetByIdAsync(
                               DataFactory.GetCommentEntity().Id,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(DataFactory.GetCommentEntity());

            repositoryManager.Setup(r => r.Comments.GetAll(It.IsAny<bool>()))
                             .Returns(DataFactory.GetComments().BuildMock());

            repositoryManager.Setup(r => r.Comments.GetCommentByIdAndUserIdAsync(
                              DataFactory.GetCommentEntity().UserId,
                              DataFactory.GetCommentEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(DataFactory.GetCommentEntity());

            return repositoryManager;
        }
    }
}

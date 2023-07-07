using MeetUp.CommentsService.Api.Controllers;
using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using Moq;

namespace MeetUp.CommentsService.Tests.UnitTests.ControllersTests
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _commentsService;
        private readonly CommentsController _commentsController;

        public CommentControllerTests()
        {
            _commentsService = MockConfigure.CreateCommentService();

            _commentsController = new CommentsController(_commentsService.Object);
            _commentsController.CreateCommentsControllerRequestMock();
        }

        [Fact]
        public async Task GetAllCommentsAsync_WithCorrectData_ShouldReturnSuccessResult()
        {
            //Arrange
            var commentQueryDto = new CommentQueryDto();

            //Act
            var result = await _commentsController.GetAllCommentsAsync(commentQueryDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _commentsService.Verify(r => r.GetAllCommentsAsync(commentQueryDto, CancellationToken.None));
        }

        [Fact]
        public async Task GetCommentByIdAsync_WithCorrectData_ShouldReturnSuccessResult()
        {
            //Arrange
            var commentEntity = DataFactory.GetCommentEntity();

            //Act
            var result = await _commentsController.GetCommentByIdAsync(commentEntity.Id, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _commentsService.Verify(r => r.GetCommentByIdAsync(commentEntity.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CreateCommentAsync_WithCorrectData_ShouldReturnSuccessResult()
        {
            //Arrange
            var commentDto = DataFactory.GetCommentDto();

            //Act
            var result = await _commentsController.CreateCommentAsync(commentDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _commentsService.Verify(r => r.CreateCommentByUserIdAsync(DataFactory.GetUserId, commentDto, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCommentByIdAsync_WithCorrectData_ShouldReturnSuccessResult()
        {
            //Arrange
            var commentId = DataFactory.GetCommentEntity().Id;

            //Act
            var result = await _commentsController.DeleteCommentByIdAsync(commentId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _commentsService.Verify(r => r.DeleteCommentByIdAndUserIdAsync(DataFactory.GetUserId, commentId, CancellationToken.None));
        }
    }
}

using FluentValidation;
using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.Hubs;
using MeetUp.CommentsService.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using Moq;
using MeetUp.CommentsService.Infrastructure.Models;
using MeetUp.CommentsService.Application.Utils.Excaption;

namespace MeetUp.CommentsService.Tests.UnitTests.ServicesTests
{
    public class CommentServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManager;
        private readonly Mock<IValidator<CommentDto>> _commentValidator;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IHubContext<CommentsHub>> _mockHubContext;

        private readonly ICommentService _commentService;

        public CommentServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _commentValidator = new Mock<IValidator<CommentDto>>();
            _repositoryManager = RepositoryManagerMock.Create();
            _mockHubContext = HubContextMock.Create();

            _commentService = new OverridedCommentsService(_repositoryManager.Object, _commentValidator.Object, _configuration.Object, _mockHubContext.Object);
        }

        [Fact]
        public async Task GetCommentByIdAsync_WhenCommentExists_ShouldReturnOutputCommentDto()
        {
            //Arrange
            var commentId = DataFactory.GetCommentEntity().Id;

            //Act
            var result = await _commentService.GetCommentByIdAsync(commentId, CancellationToken.None);

            //Assert
            Assert.Equal(commentId, result.Id);
        }

        [Fact]
        public async Task GetAllCommentsAsync_WhenCommentExists_ShouldReturnListOfOutputCommentDto()
        {
            //Arrange
            var commentQuery = DataFactory.GetCommentQueryDto();

            //Act
            var result = await _commentService.GetAllCommentsAsync(commentQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.Count(), result.MetaData.TotalCount);
        }

        [Fact]
        public async Task CreateCommentByUserIdAsync_WhenEventExists_ShouldReturnCreatedCommentId()
        {
            //Arrange
            var commentDto = DataFactory.GetCommentDto();
            var userId = DataFactory.GetUserId;

            //Act
            var result = await _commentService.CreateCommentByUserIdAsync(userId, commentDto, CancellationToken.None);

            //Assert
            Assert.NotEmpty(result.ToString());
            _repositoryManager.Verify(lw => lw.Comments.AddAsync(It.IsAny<Comment>(), CancellationToken.None));
        }

        [Fact]
        public async Task CreateCommentByUserIdAsync_WhenEventDoesNotExist_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var commentDto = new CommentDto
            {
                EventId = Guid.Empty,
                Text = "TestText"
            };

            var userId = DataFactory.GetUserId;

            //Act
            var createCommentByUserIdAsyncProcess = _commentService.CreateCommentByUserIdAsync(userId, commentDto, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await createCommentByUserIdAsyncProcess);
        }

        [Fact]
        public async Task DeleteCommentByIdAndUserIdAsync_WhenCommentExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var comment = DataFactory.GetCommentEntity();

            //Act
            await _commentService.DeleteCommentByIdAndUserIdAsync(comment.UserId, comment.Id, CancellationToken.None);

            //Assert
            _repositoryManager.Verify(lw => lw.Comments.RemoveAsync(It.IsAny<Comment>(), CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCommentByIdAndUserIdAsync_WhenCommentDoesNotExist_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var comment = DataFactory.GetCommentEntity();

            _repositoryManager.Setup(r => r.Comments.GetCommentByIdAndUserIdAsync(
                              DataFactory.GetCommentEntity().UserId,
                              DataFactory.GetCommentEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(default(Comment));
            //Act
            var deleteCommentByIdAndUserIdAsyncProcess = _commentService.DeleteCommentByIdAndUserIdAsync(comment.UserId, comment.Id, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await deleteCommentByIdAndUserIdAsyncProcess);
        }
    }
}

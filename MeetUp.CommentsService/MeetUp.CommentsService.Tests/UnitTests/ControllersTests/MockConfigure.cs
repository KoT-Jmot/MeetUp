using MeetUp.CommentsService.Api.Controllers;
using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MeetUp.CommentsService.Tests.UnitTests.ControllersTests
{
    public static class MockConfigure
    {
        public static Mock<ICommentService> CreateCommentService()
        {
            var commentService = new Mock<ICommentService>();


            commentService.Setup(r => r.GetAllCommentsAsync(
                           It.IsAny<CommentQueryDto>(),
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(DataFactory.GetAllOutputCommentsDto());

            commentService.Setup(r => r.GetCommentByIdAsync(
                           DataFactory.GetCommentEntity().Id,
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(DataFactory.GetOutputCommentDto());

            commentService.Setup(r => r.CreateCommentByUserIdAsync(
                           DataFactory.GetUserId,
                           It.IsAny<CommentDto>(),
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(DataFactory.GetCommentEntity().Id);

            return commentService;
        }

        public static void CreateCommentsControllerRequestMock(this CommentsController eventsController)
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            mockRequest.Setup(r => r.Headers).Returns(new HeaderDictionary
            {
                {"claims_UserId", DataFactory.GetUserId }
            });

            eventsController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };
        }
    }
}
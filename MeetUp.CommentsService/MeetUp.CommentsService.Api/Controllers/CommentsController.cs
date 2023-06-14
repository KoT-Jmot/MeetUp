using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Api.Features;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.CommentsService.Api.Controllers
{
    [Route("comment")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentManager;

        public CommentsController(ICommentService commentManager)
        {
            _commentManager = commentManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsAsync(
            [FromQuery] CommentQueryDto commentQuery,
            CancellationToken cancellationToken)
        {
            var comments = await _commentManager.GetAllCommentsAsync(commentQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputCommentDto>>(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentByIdAsync(
            [FromRoute] Guid commentId,
            CancellationToken cancellationToken)
        {
            var comment = await _commentManager.GetCommentByIdAsync(commentId, cancellationToken);

            return Ok(comment);
        }
    }
}

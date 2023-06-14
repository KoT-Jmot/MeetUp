using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;

namespace MeetUp.CommentsService.Application.Contracts
{
    public interface ICommentService
    {
        Task<PagedList<OutputCommentDto>> GetAllCommentsAsync(
            CommentQueryDto commentQuery,
            CancellationToken cancellationToken);

        Task<OutputCommentDto> GetCommentByIdAsync(
            Guid commentId,
            CancellationToken cancellationToken);

        Task<Guid> CreateCommentByUserIdAsync(
            string userId,
            CommentDto commentDto,
            CancellationToken cancellationToken);

        Task DeleteCommentByIdAndUserIdAsync(
            string userId,
            Guid commentId,
            CancellationToken cancellationToken);
    }
}

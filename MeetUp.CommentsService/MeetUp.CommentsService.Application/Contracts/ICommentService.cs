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

        Task<PagedList<OutputCommentDto>> GetCommentsByUserIdAsync(
            string userId,
            CancellationToken cancellationToken);

        Task<PagedList<OutputCommentDto>> GetCommentsByEventIdAsync(
            Guid eventId,
            CancellationToken cancellationToken);

        Task<Guid> CreateCommentAsync(
            CommentDto commentDto,
            CancellationToken cancellationToken);

        Task DeleteCommentByIdAsync(
            Guid commentId,
            CancellationToken cancellationToken);
    }
}

using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.DTOs.OutputDto;

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
            Guid commentId,
            CancellationToken cancellationToken);

        Task<Guid> CreateCommentAsync(
            CommentDto commentDto,
            CancellationToken cancellationToken);

        Task DeleteCommentByIdAsync(
            Guid commentId,
            CancellationToken cancellationToken);
    }
}

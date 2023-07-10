using MeetUp.CommentsService.Infrastructure.Models;

namespace MeetUp.CommentsService.Infrastructure.Contracts
{
    public interface ICommentsRepository : IBaseRepository<Comment>
    {
        Task<Comment?> GetCommentByIdAndUserIdAsync(
            string userId,
            Guid eventId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<Comment?>> GetCommentsByEventIdAsync(
            Guid eventId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);
    }
}

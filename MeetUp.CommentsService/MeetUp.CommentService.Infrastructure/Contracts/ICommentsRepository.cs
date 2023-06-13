using MeetUp.CommentsService.Infrastructure.Models;

namespace MeetUp.CommentsService.Infrastructure.Contracts
{
    public interface ICommentsRepository : IBaseRepository<Comment>
    {
        IQueryable<Comment?> GetCommentsByEventId(
            Guid eventId,
            bool trackChanges = false);
        IQueryable<Comment?> GetCommentsByUserId(
            string userId,
            bool trackChanges = false);
    }
}

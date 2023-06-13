using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;

namespace MeetUp.CommentsService.Infrastructure.Repositories
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Comment> GetCommentsByEventId(Guid eventId, bool trackChanges = false)
        {
            return GetByQueryable(c => c.EventId!.Equals(eventId), trackChanges);
        }

        public IQueryable<Comment?> GetCommentsByUserId(string userId, bool trackChanges = false)
        {
            return GetByQueryable(c => c.UserId!.Equals(userId), trackChanges);
        }
    }
}

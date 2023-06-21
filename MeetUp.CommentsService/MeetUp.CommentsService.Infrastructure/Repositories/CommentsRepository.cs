using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.CommentsService.Infrastructure.Repositories
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Comment?> GetCommentByIdAndUserIdAsync(
            string userId,
            Guid commentId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetByQueryable(c => c.UserId!.Equals(userId) && c.Id.Equals(commentId), trackChanges).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Comment?>> GetCommentsByEventIdAsync(
            Guid eventId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetByQueryable(c => c.EventId!.Equals(eventId), trackChanges).ToArrayAsync(cancellationToken);
        }
    }
}

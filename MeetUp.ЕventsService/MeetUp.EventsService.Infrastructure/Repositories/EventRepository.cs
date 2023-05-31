using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.EventsService.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Event?> GetEventByIdAndUserIdAsync(
            Guid eventId,
            string userId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetByQueryable(p => p.Id.Equals(eventId) && p.SponsorId!.Equals(userId), trackChanges).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

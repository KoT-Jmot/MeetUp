using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

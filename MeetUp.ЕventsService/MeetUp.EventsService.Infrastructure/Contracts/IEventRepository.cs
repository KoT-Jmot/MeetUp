using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Infrastructure.Contracts
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event?> GetEventByIdAndUserIdAsync(
            Guid eventId,
            string userId,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);
    }
}

using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;

namespace MeetUp.EventsService.Application.Contracts
{
    public interface IEventService
    {
        Task<Guid> CreateEventBySponserIdAsync(
            EventDto eventDto,
            string sponserId,
            CancellationToken cancellationToken);

        Task DeleteEventByIdAndSponserIdAsync(
            string sponserId,
            Guid eventId,
            CancellationToken cancellationToken);

        Task<Guid> UpdateEventByIdAndSponserIdAsync(
            Guid eventId,
            EventDto eventDto,
            string sponserId,
            CancellationToken cancellationToken);
    }
}

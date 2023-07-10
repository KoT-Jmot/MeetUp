using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;

namespace MeetUp.EventsService.Application.Contracts
{
    public interface IEventService
    {
        Task<PagedList<OutputEventDto>> GetAllEventsAsync(
            EventQueryDto eventQuery,
            CancellationToken cancellationToken);
        Task<OutputEventDto> GetEventByIdAsync(
            Guid eventId,
            CancellationToken cancellationToken);

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

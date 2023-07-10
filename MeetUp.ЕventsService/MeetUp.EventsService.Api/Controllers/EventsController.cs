using MeetUp.EventsService.Api.Features;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;
using MeetUp.EventsService.Application.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.EventsService.Api.Controllers
{
    [Route("event")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventManager;

        public EventsController(IEventService eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync(
                    [FromQuery] EventQueryDto eventQuery,
                    CancellationToken cancellationToken)
        {
            var exectlEvent = await _eventManager.GetAllEventsAsync(eventQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputEventDto>>(exectlEvent);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventByIdAsync(
            [FromRoute] Guid eventId,
            CancellationToken cancellationToken)
        {
            var product = await _eventManager.GetEventByIdAsync(eventId, cancellationToken);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(
            [FromBody] EventDto eventDto,
            CancellationToken cancellationToken)
        {
            var userId = Request.Headers[ClaimsConfiguration.UserId];

            var eventId = await _eventManager.CreateEventBySponserIdAsync(eventDto, userId, cancellationToken);

            return Created(nameof(CreateEventAsync), eventId);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventByIdAsync(
            [FromRoute] Guid eventId,
            CancellationToken cancellationToken)
        {
            var userId = Request.Headers[ClaimsConfiguration.UserId];

            await _eventManager.DeleteEventByIdAndSponserIdAsync(userId, eventId, cancellationToken);

            return NoContent();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateProductAsync(
            [FromRoute] Guid eventId,
            [FromBody] EventDto eventDto,
            CancellationToken cancellationToken)
        {
            var userId = Request.Headers[ClaimsConfiguration.UserId];

            var updatedEventId = await _eventManager.UpdateEventByIdAndSponserIdAsync(eventId, eventDto, userId, cancellationToken);

            return Ok(updatedEventId);
        }
    }
}

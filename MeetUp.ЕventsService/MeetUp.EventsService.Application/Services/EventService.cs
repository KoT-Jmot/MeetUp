using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.Utils.Excaption;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using FluentValidation;
using Mapster;
using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.Validation;

namespace MeetUp.EventsService.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IValidator<EventDto> _eventValidator;
        public EventService(
            IRepositoryManager repositoryManager,
            IValidator<EventDto> eventValidator)
        {
            _repositoryManager = repositoryManager;
            _eventValidator = eventValidator;
        }

        public async Task<Guid> CreateEventBySponserIdAsync(
        EventDto eventDto,
        string sponserId,
            CancellationToken cancellationToken)
        {
            await _eventValidator.ValidateAndThrowAsync(eventDto, cancellationToken);

            var category = await _repositoryManager.Categories.GetByIdAsync(eventDto.CategoryId, trackChanges: false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var newEvent = eventDto.Adapt<Event>();
            newEvent.CreateDate = DateTime.UtcNow;
            newEvent.SponsorId = sponserId;

            await _repositoryManager.Events.AddAsync(newEvent, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);

            return newEvent.Id;
        }

        public async Task DeleteEventByIdAndSponserIdAsync(
            string sponserId,
            Guid eventId,
            CancellationToken cancellationToken)
        {
            var deletingEvent = await _repositoryManager.Events.GetEventByIdAndUserIdAsync(eventId, sponserId, trackChanges: false, cancellationToken);

            if (deletingEvent is null)
                throw new EntityNotFoundException("Event was not found!");

            await _repositoryManager.Events.RemoveAsync(deletingEvent, cancellationToken);
            await _repositoryManager.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> UpdateEventByIdAndSponserIdAsync(
            Guid eventId,
            EventDto eventDto,
            string sponserId,
            CancellationToken cancellationToken)
        {
            await _eventValidator.ValidateAndThrowAsync(eventDto, cancellationToken);

            var category = await _repositoryManager.Categories.GetByIdAsync(eventDto.CategoryId, trackChanges: false, cancellationToken);

            if (category is null)
                throw new EntityNotFoundException("Category was not found!");

            var updatingEvent = await _repositoryManager.Events.GetByIdAsync(eventId, trackChanges: true, cancellationToken);

            if (updatingEvent is null)
                throw new EntityNotFoundException("Event was not found!");

            if (!updatingEvent.SponsorId!.Equals(sponserId))
                throw new RequestAccessException();

            updatingEvent = eventDto.Adapt<Event>();
            updatingEvent.CreateDate = DateTime.UtcNow;

            return eventId;
        }
    }
}

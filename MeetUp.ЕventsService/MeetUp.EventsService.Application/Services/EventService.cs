using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.Utils.Excaption;
using MeetUp.EventsService.Application.RequestFeatures;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using FluentValidation;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedList<OutputEventDto>> GetAllEventsAsync(EventQueryDto eventQuery, CancellationToken cancellationToken)
        {
            var events = _repositoryManager.Events.GetAll();

            if (!eventQuery.Title.IsNullOrEmpty())
                events = events.Where(p => p.Title!.Contains(eventQuery.Title!));

            if (!eventQuery.Description.IsNullOrEmpty())
                events = events.Where(p => p.Description!.Contains(eventQuery.Description!));

            if (!eventQuery.Place.IsNullOrEmpty())
                events = events.Where(p => p.Place!.Contains(eventQuery.Place!));

            if (eventQuery.CategoryId is not null)
                events = events.Where(p => p.CategoryId.Equals(eventQuery.CategoryId));

            events = events.OrderBy(p => p.DateStart);

            var totalCount = await events.CountAsync(cancellationToken);

            var pagingProducts = await events
                                        .Skip((eventQuery.PageNumber - 1) * eventQuery.PageSize)
                                        .Take(eventQuery.PageSize)
                                        .ToListAsync(cancellationToken);

            var outputProducts = pagingProducts.Adapt<IEnumerable<OutputEventDto>>();
            var productsWithMetaData = PagedList<OutputEventDto>.ToPagedList(outputProducts, eventQuery.PageNumber, totalCount, eventQuery.PageSize);

            return productsWithMetaData;
        }

        public async Task<OutputEventDto> GetEventByIdAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var exectlEvent = await _repositoryManager.Events.GetByIdAsync(eventId, trackChanges: false, cancellationToken);

            if (exectlEvent is null)
                throw new EntityNotFoundException("Event was not found!");

            var outputProduct = exectlEvent.Adapt<OutputEventDto>();

            return outputProduct;
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

using FluentValidation;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.Services;
using MeetUp.EventsService.Application.Utils.Excaption;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using MeetUp.Kafka.Contracts;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ServicesTests.EventServiceTests
{
    public class EventServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManager;
        private readonly Mock<IValidator<EventDto>> _eventValidator;
        private readonly Mock<IKafkaProducer<string, Guid>> _kafkaEventsProducer;

        private readonly IEventService _eventService;

        public EventServiceTests()
        {
            _repositoryManager = RepositoryManagerMock.CreateForEventTests();
            _eventValidator = new Mock<IValidator<EventDto>>();

            _kafkaEventsProducer = new Mock<IKafkaProducer<string, Guid>>();
            _kafkaEventsProducer.Setup(r => r.ProduceAsync(It.IsAny<string>(), It.IsAny<Guid>()));

            _eventService = new EventService(_repositoryManager.Object, _eventValidator.Object, _kafkaEventsProducer.Object);
        }

        [Fact]
        public async Task GetEventByIdAsync_WhenEventExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventId = eventEntity.Id;

            //Act
            var result = await _eventService.GetEventByIdAsync(eventId, CancellationToken.None);

            //Assert
            Assert.Equal(eventId, result.Id);
        }

        [Fact]
        public async Task GetEventByIdAsync_WhenEventDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventId = eventEntity.Id;

            _repositoryManager.Setup(r => r.Events.GetByIdAsync(
                              EventDataFactory.GetEventEntity().Id,
                              It.IsAny<bool>(),
                              It.IsAny<CancellationToken>()))
                             .ReturnsAsync(default(Event));

            //Act
            var getEventByIdAsyncProcess = _eventService.GetEventByIdAsync(eventId, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await getEventByIdAsyncProcess);
        }

        [Fact]
        public async Task GetAllEventsAsync_WithFiltration_ShouldReturnSuccessResult()
        {
            //Arrange
            var eventQuery = EventDataFactory.GetEventQuery();

            //Act
            var result = await _eventService.GetAllEventsAsync(eventQuery, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(result.MetaData.TotalCount, result.Count());
        }

        [Fact]
        public async Task CreateEventBySponserIdAsync_WhenCategoryExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var sponserId = EventDataFactory.GetUserId;
            var eventDto = EventDataFactory.GetEventDto();

            //Act
            var result = await _eventService.CreateEventBySponserIdAsync(eventDto, sponserId, CancellationToken.None);

            //Assert
            _repositoryManager.Verify(lw => lw.Categories.GetByIdAsync(
                               eventDto.CategoryId.Value,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()));

            _repositoryManager.Verify(lw => lw.Events.AddAsync(
                               It.IsAny<Event>(),
                               It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task CreateEventBySponserIdAsync_WhenCategoryDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var sponserId = EventDataFactory.GetUserId;
            var eventDto = EventDataFactory.GetEventDto();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               eventDto.CategoryId.Value,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Category));

            //Act
            var createEventBySponserIdAsyncProcess = _eventService.CreateEventBySponserIdAsync(eventDto, sponserId, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await createEventBySponserIdAsyncProcess);
        }

        [Fact]
        public async Task DeleteEventByIdAndSponserIdAsync_WhenEventExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var prducerKey = "deleteComments";
            //Act
            await _eventService.DeleteEventByIdAndSponserIdAsync(eventEntity.SponsorId, eventEntity.Id, CancellationToken.None);

            //Assert
            _repositoryManager.Verify(lw => lw.Events.RemoveAsync(
                               It.IsAny<Event>(),
                               It.IsAny<CancellationToken>()));

            _kafkaEventsProducer.Verify(lw => lw.ProduceAsync(prducerKey, eventEntity.Id));
        }

        [Fact]
        public async Task DeleteEventByIdAndSponserIdAsync_WhenEventDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var prducerKey = "deleteComments";

            _repositoryManager.Setup(r => r.Events.GetEventByIdAndUserIdAsync(
                               eventEntity.Id,
                               eventEntity.SponsorId,
                               false,
                               CancellationToken.None))
                              .ReturnsAsync(default(Event));

            //Act
            var deleteEventByIdAndSponserIdAsyncProcess = _eventService.DeleteEventByIdAndSponserIdAsync(eventEntity.SponsorId, eventEntity.Id, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await deleteEventByIdAndSponserIdAsyncProcess);
        }

        [Fact]
        public async Task UpdateEventByIdAndSponserIdAsync_WhenAllIsOk_ShouldReturnSuccessResult()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventDto = EventDataFactory.GetEventDto();

            //Act
            var result =
               await _eventService.UpdateEventByIdAndSponserIdAsync(
                                   eventEntity.Id,
                                   eventDto,
                                   eventEntity.SponsorId,
                                   CancellationToken.None);

            //Assert
            Assert.Equal(eventEntity.Id, result);
        }

        [Fact]
        public async Task UpdateEventByIdAndSponserIdAsync_WhenCategoryDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventDto = EventDataFactory.GetEventDto();

            _repositoryManager.Setup(r => r.Categories.GetByIdAsync(
                               eventDto.CategoryId.Value,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Category));

            //Act
            var updateEventByIdAndSponserIdAsyncProcess =
                _eventService.UpdateEventByIdAndSponserIdAsync(
                              eventEntity.Id,
                              eventDto,
                              eventEntity.SponsorId,
                              CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await updateEventByIdAndSponserIdAsyncProcess);
        }

        [Fact]
        public async Task UpdateEventByIdAndSponserIdAsync_WhenEventDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventDto = EventDataFactory.GetEventDto();

            _repositoryManager.Setup(r => r.Events.GetByIdAsync(
                               eventEntity.Id,
                               It.IsAny<bool>(),
                               It.IsAny<CancellationToken>()))
                              .ReturnsAsync(default(Event));

            //Act
            var updateEventByIdAndSponserIdAsyncProcess =
                _eventService.UpdateEventByIdAndSponserIdAsync(
                              eventEntity.Id,
                              eventDto,
                              eventEntity.SponsorId,
                              CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await updateEventByIdAndSponserIdAsyncProcess);
        }

        [Fact]
        public async Task UpdateEventByIdAndSponserIdAsync_WhenUserIsNotCorrect_ShouldReturnRequestAccessException()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();
            var eventDto = EventDataFactory.GetEventDto();

            //Act
            var updateEventByIdAndSponserIdAsyncProcess =
                _eventService.UpdateEventByIdAndSponserIdAsync(
                              eventEntity.Id,
                              eventDto,
                              Guid.NewGuid().ToString(),
                              CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<RequestAccessException>(async () => await updateEventByIdAndSponserIdAsyncProcess);
        }
    }
}

using MeetUp.EventsService.Api.Controllers;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using Moq;

namespace MeetUp.EventsService.Tests.UnitTests.ControllersTests
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _eventService;
        private readonly EventsController _eventsController;

        public EventControllerTests()
        {
            _eventService = MockConfigure.CreateEventService();

            _eventsController = new EventsController(_eventService.Object);
            _eventsController.CreateEventControllerRequestMock();
        }

        [Fact]
        public async Task GetAllEventsAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var eventQueryDto = new EventQueryDto();

            //Act
            var result = await _eventsController.GetAllEventsAsync(eventQueryDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _eventService.Verify(r => r.GetAllEventsAsync(eventQueryDto, CancellationToken.None));
        }

        [Fact]
        public async Task GetEventByIdAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var eventId = EventDataFactory.GetOutputEventDto().Id;
            //Act
            var result = await _eventsController.GetEventByIdAsync(eventId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _eventService.Verify(r => r.GetEventByIdAsync(eventId, CancellationToken.None));
        }

        [Fact]
        public async Task CreateEventAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var eventDto = EventDataFactory.GetEventDto();

            //Act
            var result = await _eventsController.CreateEventAsync(eventDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _eventService.Verify(r => r.CreateEventBySponserIdAsync(eventDto, EventDataFactory.GetUserId, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteEventByIdAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var eventEntity = EventDataFactory.GetEventEntity();

            //Act
            var result = await _eventsController.DeleteEventByIdAsync(eventEntity.Id, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _eventService.Verify(r => r.DeleteEventByIdAndSponserIdAsync(eventEntity.SponsorId, eventEntity.Id, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateProductAsync_WithCorrectData_ShouldReturnSuccessfulActionResult()
        {
            //Arrange
            var eventDto = EventDataFactory.GetEventDto();
            var eventEntity = EventDataFactory.GetEventEntity();

            //Act
            var result = await _eventsController.UpdateProductAsync(eventEntity.Id, eventDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            _eventService.Verify(r => r.UpdateEventByIdAndSponserIdAsync(eventEntity.Id, eventDto, eventEntity.SponsorId, CancellationToken.None));
        }
    }
}

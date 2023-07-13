using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.HangFireSerivce.Application.Services;
using MeetUp.Kafka.Contracts;
using Moq;

namespace MeetUp.HangFireService.Tests.UnitTests
{
    public class HangFireServiceTests
    {
        private readonly Mock<IKafkaProducer<string, DateTime>> _kafkaEventsProducer;
        private readonly INotificationServices _notificationServices;

        public HangFireServiceTests()
        {
            _kafkaEventsProducer = new Mock<IKafkaProducer<string, DateTime>>();
            _notificationServices = new NotificationServices(_kafkaEventsProducer.Object);
        }

        [Fact]
        public async Task DeleteLatestOrdersAsync_WithCorrectData_ShouldReturnSuccessResult()
        {
            //Arrange
            var hoursInterval = 0;
            var kafkaKey = "deletedEventsDate";

            //Act
            await _notificationServices.DeleteLatestOrdersAsync(hoursInterval);

            //Assert
            _kafkaEventsProducer.Verify(lw => lw.ProduceAsync(kafkaKey, It.IsAny<DateTime>()));
        }
    }
}

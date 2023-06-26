using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.Kafka.Contracts;
using Microsoft.Extensions.Logging;

namespace MeetUp.HangFireSerivce.Application.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly IKafkaProducer<string, DateTime> _kafkaEventsProducer;
        public NotificationServices(IKafkaProducer<string, DateTime> kafkaEventsProducer) 
        {
            _kafkaEventsProducer = kafkaEventsProducer;
        }

        public async Task DeleteLatestOrdersAsync(int HoursInterval = 0)
        {
            await _kafkaEventsProducer.ProduceAsync("deletedEventsDate", DateTime.UtcNow.AddHours(HoursInterval));
        }
    }
}
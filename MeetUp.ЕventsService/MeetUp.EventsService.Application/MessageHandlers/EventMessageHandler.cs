using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.Kafka.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.EventsService.Application.MessageHandlers
{
    public class OldEventMessageHandler : IKafkaConsumerHandler<string, DateTime>
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly IKafkaProducer<string, Guid> _kafkaEventsProducer;

        public OldEventMessageHandler(IRepositoryManager repositoryManager, IKafkaProducer<string, Guid> kafkaEventsProducer)
        {
            _repositoryManager = repositoryManager;
            _kafkaEventsProducer = kafkaEventsProducer;
        }

        public async Task HandleAsync(string key, DateTime value)
        {
            var oldEvents = await _repositoryManager.Events.GetAll().Where(c=>c.DateStart<value).ToArrayAsync();

            if (oldEvents.Count() is not 0)
            {
                foreach(var oldEvent in oldEvents)
                {
                    await _repositoryManager.Events.RemoveAsync(oldEvent);
                    await _kafkaEventsProducer.ProduceAsync("deleteComments", oldEvent.Id);
                }

                await _repositoryManager.SaveChangesAsync();
            }
        }
    }
}


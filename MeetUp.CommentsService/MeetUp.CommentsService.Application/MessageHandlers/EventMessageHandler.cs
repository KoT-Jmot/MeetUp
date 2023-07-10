using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.Kafka.Contracts;

namespace MeetUp.CommentsService.Application.MessageHandlers
{
    public class EventMessageHandler : IKafkaConsumerHandler<string, Guid>
    {
        private readonly IRepositoryManager _repositoryManager;

        public EventMessageHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task HandleAsync(string key, Guid value)
        {
            var eventComments = await _repositoryManager.Comments.GetCommentsByEventIdAsync(value);

            if (eventComments is not null)
            {
                await _repositoryManager.Comments.RemoveRangeAsync(eventComments!);
                await _repositoryManager.SaveChangesAsync();
            }
        }
    }
}

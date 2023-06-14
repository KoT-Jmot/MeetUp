using Grpc.Core;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUpGrpc;

namespace MeetUp.EventsService.Application.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly IRepositoryManager _repositoryManager;
        public GreeterService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public override Task<EventReply> EventExist(EventRequest request,
        ServerCallContext context)
        {
            return Task.FromResult(new EventReply
            {
                Message = _repositoryManager.Events.GetByIdAsync(Guid.Parse(request.EventId)).GetAwaiter().GetResult() is not null
            });
        }
    }
}

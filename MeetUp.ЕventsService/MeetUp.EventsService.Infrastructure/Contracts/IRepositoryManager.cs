namespace MeetUp.EventsService.Infrastructure.Contracts
{
    public interface IRepositoryManager
    {
        IEventRepository Events { get; }
        ICategoryRepository Categories { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

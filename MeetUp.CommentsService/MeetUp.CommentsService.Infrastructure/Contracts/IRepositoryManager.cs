namespace MeetUp.CommentsService.Infrastructure.Contracts
{
    public interface IRepositoryManager
    {

        ICommentsRepository Comments { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

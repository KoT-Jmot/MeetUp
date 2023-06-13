using MeetUp.CommentsService.Infrastructure.Contracts;

namespace MeetUp.CommentsService.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;

        private ICommentsRepository? _commentsRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }
        public ICommentsRepository Comments =>
            _commentsRepository ??= new CommentsRepository(_context);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

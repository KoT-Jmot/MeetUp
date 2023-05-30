using MeetUp.EventsService.Infrastructure.Contracts;

namespace MeetUp.EventsService.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;


        private IEventRepository? _eventRepository;
        private ICategoryRepository? _categoryRepository;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }

        public IEventRepository Events =>
            _eventRepository ??= new EventRepository(_context);
        public ICategoryRepository Catigories =>
            _categoryRepository ??= new CategoryRepository(_context);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

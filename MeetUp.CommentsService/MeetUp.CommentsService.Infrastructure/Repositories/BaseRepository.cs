using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeetUp.CommentsService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> Set;

        protected BaseRepository(ApplicationContext context)
        {
            Set = context.Set<T>();
        }

        public IQueryable<T> GetAll(bool trackChanges = false)
        {
            return GetByQueryable(_ => true, trackChanges);
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return trackChanges
                        ? await Set.AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id), cancellationToken)
                        : await Set.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id), cancellationToken);
        }

        public async Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await Set.AddAsync(entity, cancellationToken);
        }

        public async Task RemoveAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Set.Remove(entity), cancellationToken);
        }

        public async Task RemoveRangeAsync(
            IEnumerable<T> entity,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Set.RemoveRange(entity), cancellationToken);
        }

        protected virtual IQueryable<T> GetByQueryable(
            Expression<Func<T, bool>> expression,
            bool trackChanges = false)
        {
            var items = Set.Where(expression);

            return trackChanges
                        ? items.AsTracking()
                        : items.AsNoTracking();
        }
    }
}

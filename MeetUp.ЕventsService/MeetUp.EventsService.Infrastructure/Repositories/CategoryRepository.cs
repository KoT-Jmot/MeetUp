using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.EventsService.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Category?> GetCategoryByNameAsync(
            string name,
            bool trackChanges = false,
            CancellationToken cancellationToken = default)
        {
            return await GetByQueryable(c => c.Name!.Equals(name), trackChanges).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

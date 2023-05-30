using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

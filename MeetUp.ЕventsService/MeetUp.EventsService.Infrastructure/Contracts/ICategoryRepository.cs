using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Infrastructure.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(
            string name,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);
    }
}

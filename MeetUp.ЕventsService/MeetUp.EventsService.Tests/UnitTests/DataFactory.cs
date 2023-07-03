using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Tests.UnitTests
{
    public static class DataFactory
    {
        public static IEnumerable<Category> GetAllCategoryEntity()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    Name = "First"
                },
                new Category()
                {
                    Id = Guid.Parse("4a4976bd-ab40-469f-374c-08db716a571a"),
                    Name = "Second"
                },
                new Category()
                {
                    Id = Guid.Parse("0cd958e0-0acd-4f4a-8fe6-319bcddf59e0"),
                    Name = "Third"
                }
            };
        }

        public static Category GetCategoryEntity()
        {
            return new Category()
            {
                Id = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                Name = "First"
            };
        }

        public static CategoryDto GetCategoryDto()
        {
            return new CategoryDto()
            {
                Name = "First"
            };
        }
    }
}

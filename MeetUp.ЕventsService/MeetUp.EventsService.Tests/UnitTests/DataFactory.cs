using MeetUp.EventsService.Application.DTOs.InputDto.CategoryDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;
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

        public static PagedList<OutputCategoryDto> GetAllOutputCategoriesDto()
        {
            var outputCategories = new List<OutputCategoryDto>()
            {
                new OutputCategoryDto()
                {
                    Id = Guid.Parse("21f85435-204d-4e1f-80bb-08db734a088b"),
                    Name = "First"
                },
                new OutputCategoryDto()
                {
                    Id = Guid.Parse("52fc493e-ed46-4df7-5544-08db734a420d"),
                    Name = "Second"
                },
                new OutputCategoryDto()
                {
                    Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                    Name = "Third"
                }
            };

            return PagedList<OutputCategoryDto>.ToPagedList(outputCategories, 3, 1, 3);
        }

        public static OutputCategoryDto GetOutputCategoryDto()
        {
            return new OutputCategoryDto()
            {
                Id = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                Name = "First"
            };
        }
    }
}

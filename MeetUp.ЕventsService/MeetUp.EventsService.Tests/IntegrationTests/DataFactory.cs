using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Tests.IntegrationTests
{
    public static class DataFactory
    {
        public static string GetUserId => "21f85435-204d-4e1f-80bb-08db734a088b";
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
        public static IEnumerable<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Id = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    Title = "First",
                    Description = "TestDescription",
                    Place = "Minsk",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    CreateDate = DateTime.Now.AddDays(-1),
                    SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
                },
                new Event()
                {
                    Id = Guid.Parse("52fc493e-ed46-4df7-5544-08db734a420d"),
                    Title = "Second",
                    Description = "TestDescription",
                    Place = "Minsk",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    CreateDate = DateTime.Now.AddDays(-1),
                    SponsorId = "10w12509-133d-1e1f-80bb-08da584a128x"
                },
                new Event()
                {
                    Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                    Title = "Third",
                    Description = "TestDescription",
                    Place = "Grodno",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    CreateDate = DateTime.Now.AddDays(-1),
                    SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
                }
            };
        }

        public static EventDto[] GetEventArrayForCreating()
        {
            return new EventDto[]
            {
                new EventDto()
                {
                    Title = "Second",
                    Description = "TestDescription",
                    Place = "Minsk",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1")
                },
                new EventDto()
                {
                    Title = "Third",
                    Description = "TestDescription",
                    Place = "Grodno",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b0c3-34e5-4f4a-7bbf-08db683498b1") // incorrect categoryId
                }
            };
        }

        public static EventDto[] GetEventArrayForUpdating()
        {
            return new EventDto[]
            {
                new EventDto()
                {
                    Title = "TestUpdate1",
                    Description = "TestDescription",
                    Place = "Grodno",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1")
                },
                new EventDto()
                {
                    Title = "TestUpdate2",
                    Description = "TestDescription",
                    Place = "Grodno",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("1789b0c3-34e5-4f4a-7bbf-08db683498b2") // incorrect categoryId
                }
            };
        }
    }
}

using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Application.RequestFeatures;
using MeetUp.EventsService.Infrastructure.Models;

namespace MeetUp.EventsService.Tests.UnitTests
{
    public static class EventDataFactory
    {
        public static string GetUserId => "21f85435-204d-4e1f-80bb-08db734a088b";

        public static Event GetEventEntity()
        {
            return new Event()
            {
                Id = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                Title = "First",
                Description = "TestDescription",
                Place = "Minsk",
                DateStart = DateTime.Now.AddDays(1),
                CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                CreateDate = DateTime.Now.AddDays(-1),
                SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
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
                    SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
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

        public static EventQueryDto GetEventQuery()
        {
            return new EventQueryDto()
            {
                Title = "d",
                Description = "Test",
                Place = "Minsk",
                CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1")
            };
        }

        public static EventDto GetEventDto()
        {
            return new EventDto()
            {
                Title = "First",
                Description = "TestDescription",
                Place = "Minsk",
                DateStart = DateTime.Now.AddDays(1),
                CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
            };
        }

        public static PagedList<OutputEventDto> GetOutputEventsDto()
        {
            var outputEvents = new List<OutputEventDto>()
            {
                new OutputEventDto()
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
                new OutputEventDto()
                {
                    Id = Guid.Parse("52fc493e-ed46-4df7-5544-08db734a420d"),
                    Title = "Second",
                    Description = "TestDescription",
                    Place = "Minsk",
                    DateStart = DateTime.Now.AddDays(1),
                    CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    CreateDate = DateTime.Now.AddDays(-1),
                    SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
                },
                new OutputEventDto()
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

            return PagedList<OutputEventDto>.ToPagedList(outputEvents, 3, 1, 3);
        }

        public static OutputEventDto GetOutputEventDto()
        {
            return new OutputEventDto()
            {
                Id = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                Title = "First",
                Description = "TestDescription",
                Place = "Minsk",
                DateStart = DateTime.Now.AddDays(1),
                CategoryId = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                CreateDate = DateTime.Now.AddDays(-1),
                SponsorId = "21f85435-204d-4e1f-80bb-08db734a088b"
            };
        }
    }
}

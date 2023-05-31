using MeetUp.EventsService.Application.DTOs.InputDto.EventDto;
using MeetUp.EventsService.Application.DTOs.OutputDto;
using MeetUp.EventsService.Infrastructure.Models;
using Mapster;

namespace MeetUp.EventsService.Application.Mapster
{
    public class EventsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EventDto, Event>();
            config.NewConfig<Event, OutputEventDto>();
        }
    }
}

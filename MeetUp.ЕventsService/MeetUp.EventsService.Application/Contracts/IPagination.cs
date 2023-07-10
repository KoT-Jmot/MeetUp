using MeetUp.EventsService.Application.RequestFeatures;

namespace MeetUp.EventsService.Application.Contracts
{
    public interface IPagination
    {
        MetaData? MetaData { get; set; }
    }
}

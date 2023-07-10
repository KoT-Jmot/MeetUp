using MeetUp.CommentsService.Application.RequestFeatures;

namespace MeetUp.CommentsService.Application.Contracts
{
    public interface IPagination
    {
        MetaData? MetaData { get; set; }
    }
}

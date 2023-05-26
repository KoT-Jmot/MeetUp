using MeetUp.IdentityService.Application.RequestFeatures;

namespace MeetUp.IdentityService.Application.Contracts
{
    public interface IPagination
    {
        MetaData? MetaData { get; set; }
    }
}

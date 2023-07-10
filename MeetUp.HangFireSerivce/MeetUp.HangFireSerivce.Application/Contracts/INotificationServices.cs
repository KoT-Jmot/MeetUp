namespace MeetUp.HangFireSerivce.Application.Contracts
{
    public interface INotificationServices
    {
        Task DeleteLatestOrdersAsync(int HoursInterval = 0);
    }
}

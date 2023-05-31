namespace MeetUp.EventsService.Application.Utils.Excaption
{
    public class RequestAccessException : Exception
    {
        public RequestAccessException(string message = "Access was denied!") : base(message)
        {
        }
    }
}

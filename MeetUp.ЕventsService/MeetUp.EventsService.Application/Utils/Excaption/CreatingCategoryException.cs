namespace MeetUp.EventsService.Application.Utils.Excaption
{
    public class CreatingCategoryException : Exception
    {
        public CreatingCategoryException(string? message = "Incorrect category of event!") : base(message)
        {
        }
    }
}

namespace MeetUp.CommentsService.Application.Utils.Excaption
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message = "Entity was not found!") : base(message)
        {
        }
    }
}

namespace MeetUp.CommentsService.Application.DTOs.InputDto
{
    public class CommentDto
    {
        public string Text { get; set; } = string.Empty;
        public Guid EventId { get; set; }
    }
}

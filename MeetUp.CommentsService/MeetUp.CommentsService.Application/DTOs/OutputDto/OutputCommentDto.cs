namespace MeetUp.CommentsService.Application.DTOs.OutputDto
{
    public class OutputCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public Guid EventId { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

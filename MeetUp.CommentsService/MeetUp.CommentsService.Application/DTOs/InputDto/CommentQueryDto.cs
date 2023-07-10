namespace MeetUp.CommentsService.Application.DTOs.InputDto
{
    public class CommentQueryDto : BaseQuery
    {
        public string? Text { get; set; }
        public Guid? EventId { get; set; }
        public string? UserId { get; set; }
    }
}

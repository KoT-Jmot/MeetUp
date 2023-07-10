namespace MeetUp.CommentsService.Infrastructure.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public Guid EventId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}

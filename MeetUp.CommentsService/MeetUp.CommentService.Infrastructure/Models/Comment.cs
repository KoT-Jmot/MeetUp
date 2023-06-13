namespace MeetUp.CommentsService.Infrastructure.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Guid EventId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}

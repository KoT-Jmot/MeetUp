namespace MeetUp.EventsService.Infrastructure.Models
{
    public class Event : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Place { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public string? SponsorId { get; set; }
        public Guid categoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}

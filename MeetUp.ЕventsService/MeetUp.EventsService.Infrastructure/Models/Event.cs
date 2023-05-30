namespace MeetUp.EventsService.Infrastructure.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SponsorId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public Guid categoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}

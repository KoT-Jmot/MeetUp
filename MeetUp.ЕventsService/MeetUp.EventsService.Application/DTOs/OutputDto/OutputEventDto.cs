namespace MeetUp.EventsService.Application.DTOs.OutputDto
{
    public class OutputEventDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Place { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? SponsorId { get; set; }
        public Guid categoryId { get; set; }
    }
}

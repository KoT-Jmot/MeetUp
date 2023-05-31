namespace MeetUp.EventsService.Application.DTOs.InputDto.EventDto
{
    public class EventDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Place { get; set; }
        public DateTime? DateStart { get; set; }
        public Guid categoryId { get; set; }
    }
}

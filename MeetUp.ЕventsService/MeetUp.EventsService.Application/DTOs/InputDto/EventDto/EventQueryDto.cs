namespace MeetUp.EventsService.Application.DTOs.InputDto.EventDto
{
    public class EventQueryDto : BaseQuery
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Place { get; set; }
        public Guid CategoryId { get; set; }
    }
}

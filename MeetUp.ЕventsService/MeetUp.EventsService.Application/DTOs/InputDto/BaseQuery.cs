namespace MeetUp.EventsService.Application.DTOs.InputDto
{
    public abstract class BaseQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

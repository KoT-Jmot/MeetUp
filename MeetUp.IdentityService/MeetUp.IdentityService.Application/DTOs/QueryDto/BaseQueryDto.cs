namespace MeetUp.IdentityService.Application.DTOs.QueryDto
{
    public class BaseQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

namespace MeetUp.CommentsService.Application.DTOs.InputDto
{
    public class CommentQueryDto : BaseQuery
    {
        public string Text { get; set; } = string.Empty;
    }
}

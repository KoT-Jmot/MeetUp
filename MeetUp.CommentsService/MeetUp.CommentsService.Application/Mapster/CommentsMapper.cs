using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Infrastructure.Models;
using Mapster;

namespace MeetUp.CommentsService.Application.Mapster
{
    public class CommentsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CommentDto, Comment>();
            config.NewConfig<Comment, OutputCommentDto>();
        }
    }
}

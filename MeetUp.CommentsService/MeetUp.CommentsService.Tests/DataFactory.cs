using MeetUp.CommentsService.Application.DTOs.InputDto;
using MeetUp.CommentsService.Application.DTOs.OutputDto;
using MeetUp.CommentsService.Application.RequestFeatures;
using MeetUp.CommentsService.Infrastructure.Models;

namespace MeetUp.CommentsService.Tests
{
    public static class DataFactory
    {
        public static string GetUserId => "21f85435-204d-4e1f-80bb-08db734a088b";

        public static Comment GetCommentEntity()
        {
            return new Comment
            {
                Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                CreateDate = DateTime.Now,
                Text = "TestTextForComment"
            };
        }

        public static IEnumerable<Comment> GetCommentEntities()
        {
            return new List<Comment>
            {
                new Comment
                {
                    Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Test"
                },
                new Comment
                {
                    Id = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Text"
                },
                new Comment
                {
                    Id = Guid.Parse("52fc493e-ed46-4df7-5544-08db734a420d"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Comment"
                }
            };
        }

        public static CommentQueryDto GetCommentQueryDto()
        {
            return new CommentQueryDto
            {
                EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                Text = "Te"
            };
        }
        public static CommentDto GetCommentDto()
        {
            return new CommentDto
            {
                EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                Text = "Te"
            };
        }

        public static OutputCommentDto GetOutputCommentDto()
        {
            return new OutputCommentDto
            {
                Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                CreateDate = DateTime.Now,
                Text = "Test"
            };
        }

        public static PagedList<OutputCommentDto> GetAllOutputCommentsDto()
        {
            var outputComments = new List<OutputCommentDto>()
            {
                new OutputCommentDto
                {
                    Id = Guid.Parse("c7264143-e47a-42e4-b97a-29d02088282a"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Test"
                },
                new OutputCommentDto
                {
                    Id = Guid.Parse("2789b1c3-34e5-4f4a-7bbf-08db683498b1"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Text"
                },
                new OutputCommentDto
                {
                    Id = Guid.Parse("52fc493e-ed46-4df7-5544-08db734a420d"),
                    EventId = Guid.Parse("1789b1c3-34a2-4f4a-7bbf-08db683498b1"),
                    UserId = "21f85435-204d-4e1f-80bb-08db734a088b",
                    CreateDate = DateTime.Now,
                    Text = "Comment"
                }
            };

            return PagedList<OutputCommentDto>.ToPagedList(outputComments, 3, 1, 3);
        }
    }
}

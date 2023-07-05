using MeetUp.CommentsService.Infrastructure.Models;

namespace MeetUp.CommentsService.Tests.IntegrationTests
{
    public static class DataFactory
    {
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
    }
}

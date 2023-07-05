using MeetUp.CommentsService.Application.Mapster;
using Mapster;

namespace MeetUp.CommentsService.Tests.UnitTests.MappingTests
{
    public class CommentMapsterTests
    {
        [Fact]
        public void TestMapping()
        {
            // Arrange
            var config = new TypeAdapterConfig();
            var register = new CommentsMapper();
            register.Register(config);

            // Act
            var mappings = config.RuleMap;

            // Assert
            Assert.NotEmpty(mappings);
        }
    }
}

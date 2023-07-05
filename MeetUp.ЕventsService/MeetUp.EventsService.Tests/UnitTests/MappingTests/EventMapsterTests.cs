using Mapster;
using MeetUp.EventsService.Application.Mapster;

namespace MeetUp.EventsService.Tests.UnitTests.MappingTests
{
    public class EventMapsterTests
    {
        [Fact]
        public void TestMapping()
        {
            // Arrange
            var config = new TypeAdapterConfig();
            var register = new EventsMapper();
            register.Register(config);

            // Act
            var mappings = config.RuleMap;

            // Assert
            Assert.NotEmpty(mappings);
        }
    }
}

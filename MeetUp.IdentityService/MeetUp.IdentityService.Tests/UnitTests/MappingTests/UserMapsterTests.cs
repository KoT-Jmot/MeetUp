using Mapster;
using MeetUp.IdentityService.Application.Mapster;
using Microsoft.AspNetCore.Identity;

namespace MeetUp.IdentityService.Tests.UnitTests.MappingTests
{
    public class UserMapsterTests
    {
        [Fact]
        public void TestMapping()
        {
            // Arrange
            var config = new TypeAdapterConfig();
            var register = new IdentityUserMapper();
            register.Register(config);

            // Act
            var mappings = config.RuleMap;

            // Assert
            Assert.NotEmpty(mappings);
        }
    }
}

using Microsoft.Extensions.Configuration;
using MeetUp.IdentityService.Application.Utils;
using Moq;

namespace MeetUp.IdentityService.Tests.UnitTests.ServicesTests
{
    public static class JwtConfigMock
    {
        public static Mock<JWTConfig> Create(IConfiguration configuration)
        {
            var jwtConfig = new Mock<JWTConfig>(configuration);

            jwtConfig.Setup(r => r.GetValidIssuer()).Returns("MeetUp");
            jwtConfig.Setup(r => r.GetValidAudience()).Returns("https://localhost:5001");
            jwtConfig.Setup(r => r.GetExpires()).Returns(30);

            return jwtConfig;
        }
    }
}

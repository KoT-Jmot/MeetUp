using Microsoft.AspNetCore.Http;

namespace MeetUp.HangFireService.Tests.IntegrationTests
{
    public class HangFireTests
    {
        private readonly HttpClient _client;

        public HangFireTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/hangfire", StatusCodes.Status200OK)]
        [InlineData("/hangfire123", StatusCodes.Status404NotFound)]
        public async Task HangFire_SendRequest_ShouldReturnCorrectStatusCode(
            string url,
            int statusCode)
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }
    }
}

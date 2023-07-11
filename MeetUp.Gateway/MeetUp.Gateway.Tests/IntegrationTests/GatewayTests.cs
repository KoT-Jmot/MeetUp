using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MeetUp.Gateway.Tests.IntegrationTests
{
    public class GatewayTests
    {
        private readonly HttpClient _client;

        public GatewayTests()
        {
            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/", StatusCodes.Status404NotFound)]
        [InlineData("/category", StatusCodes.Status502BadGateway)]
        public async Task RedirectRequest_SendRequest_ShouldReturnOk(
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

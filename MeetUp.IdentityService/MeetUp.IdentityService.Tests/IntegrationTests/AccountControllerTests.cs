using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MeetUp.IdentityService.Tests.IntegrationTests
{
    public class AccountControllerTests
    {
        private readonly HttpClient _client;

        public AccountControllerTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/account")]
        [InlineData("/account/test@gmail.com")]
        public async Task GetUsersAsync_SendRequest_ShouldReturnOk(string url)
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}

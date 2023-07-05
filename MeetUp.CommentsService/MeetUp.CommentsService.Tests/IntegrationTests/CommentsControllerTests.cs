using Microsoft.AspNetCore.Http;

namespace MeetUp.CommentsService.Tests.IntegrationTests
{
    public class CommentsControllerTests
    {
        private readonly HttpClient _client;

        public CommentsControllerTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/comment", StatusCodes.Status200OK)]
        [InlineData("/comment/c7264143-e47a-42e4-b97a-29d02088282a", StatusCodes.Status200OK)]
        [InlineData("/comment/c7264143-e47a-42e4-b97a-29d02088282b", StatusCodes.Status422UnprocessableEntity)]
        public async Task GetCategoriesAsync_SendRequest_ShouldReturnOk(
            string url,
            int statusCode)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace MeetUp.EventsService.Tests.IntegrationTests.EventsTests
{
    public class EventControllerTests
    {
        private readonly HttpClient _client;

        public EventControllerTests()
        {
            var webHost = FactoryConfiguration.WebApplicationFactoryConfig();

            _client = webHost.CreateClient();
        }

        [Theory]
        [InlineData("/event", StatusCodes.Status200OK)]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b1", StatusCodes.Status200OK)]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b0", StatusCodes.Status422UnprocessableEntity)]
        public async Task GetEventsAsync_SendRequest_ShouldReturnCorrectStatusCode(string url, int statusCode)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData(0, StatusCodes.Status201Created)]
        [InlineData(1, StatusCodes.Status422UnprocessableEntity)]
        public async Task CreateCategoryAsync_SendRequest_ShouldReturnCorrectStatusCode(int index, int statusCode)
        {
            // Arrange
            string url = "/event";
            var resultContentType = "application/json; charset=utf-8";

            _client.DefaultRequestHeaders.Add("claims_UserId", DataFactory.GetUserId);

            var events = DataFactory.GetEventArrayForCreating();
            var jsonData = JsonConvert.SerializeObject(events[index]);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }

        [Theory]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b1", StatusCodes.Status204NoContent)]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b2", StatusCodes.Status422UnprocessableEntity)]
        public async Task DeleteCategoryAsync_SendRequest_ShouldReturnCorrectStatusCode(string url, int statusCode)
        {
            // Arrange
            _client.DefaultRequestHeaders.Add("claims_UserId", DataFactory.GetUserId);

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }

        [Theory]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b1", 0, StatusCodes.Status200OK)]
        [InlineData("/event/1789b1c3-34a2-4f4a-7bbf-08db683498b1", 1, StatusCodes.Status422UnprocessableEntity)]
        [InlineData("/event/52fc493e-ed46-4df7-5544-08db734a420d", 0, StatusCodes.Status403Forbidden)]
        [InlineData("/event/11fc493e-ed46-4df7-5544-08db734a421m", 0, StatusCodes.Status422UnprocessableEntity)]
        public async Task UpdateProductAsync_SendRequest_ShouldReturnCorrectStatusCode(string url, int index, int statusCode)
        {
            // Arrange
            var resultContentType = "application/json; charset=utf-8";

            _client.DefaultRequestHeaders.Add("claims_UserId", DataFactory.GetUserId);

            var events = DataFactory.GetEventArrayForUpdating();
            var jsonData = JsonConvert.SerializeObject(events[index]);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(url, content);

            // Assert
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
            Assert.Equal(resultContentType, response.Content.Headers.ContentType!.ToString());
        }
    }
}

using System.Text.Json;

namespace MeetUp.EventsService.Api.Utils.Excaption
{
    public class ErrorDetails
    {
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}

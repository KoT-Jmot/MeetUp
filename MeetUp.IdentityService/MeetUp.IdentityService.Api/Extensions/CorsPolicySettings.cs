namespace MeetUp.IdentityService.Api.Extensions
{
    public class CorsPolicySettings
    {
        public string? PolicyName { get; init; }
        public string[]? AllowedOrigins { get; init; }
        public bool AllowAnyHeader { get; init; }
        public string[]? AllowedHeaders { get; init; }
        public bool AllowAnyMethod { get; init; }
        public string[]? AllowedMethods { get; init; }
    }
}

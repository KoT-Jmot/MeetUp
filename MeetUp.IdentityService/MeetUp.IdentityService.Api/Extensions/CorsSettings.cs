namespace MeetUp.IdentityService.Api.Extensions
{
    public class CorsSettings
    {
        public bool UserCors { get; init; }
        public string? UsePolicyWithName { get; init; }
        public CorsPolicySettings[]? Policies { get; init; }
    }
}

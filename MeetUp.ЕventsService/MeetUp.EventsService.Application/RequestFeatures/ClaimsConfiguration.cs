using System.Security.Claims;

namespace MeetUp.EventsService.Application.RequestFeatures
{
    public static class ClaimsConfiguration
    {
        public static string GetUserId(this ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        }
    }
}

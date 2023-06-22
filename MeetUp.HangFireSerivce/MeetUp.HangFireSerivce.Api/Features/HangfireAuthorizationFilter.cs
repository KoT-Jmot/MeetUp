using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace MeetUp.HangFireSerivce.Api.Features
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            //return httpContext.User.IsInRole("User");
            return true;
        }
    }
}

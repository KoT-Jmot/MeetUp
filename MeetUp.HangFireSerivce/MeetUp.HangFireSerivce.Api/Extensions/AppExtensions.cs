using Hangfire;
using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.HangFireSerivce.Infrastructure;

namespace MeetUp.HangFireSerivce.Api.Extensions
{
    public static class AppExtensions
    {
        public static async Task<WebApplication> InitializeHangFireContextAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var hangFireContext = scope.ServiceProvider.GetRequiredService<HangFireContext>();
                await hangFireContext.Database.EnsureCreatedAsync();
            }
            return app;
        }

        public static async Task<WebApplication> InitializeHangFireJobStorageAsync(this WebApplication app)
        {
            //await using (var scope = app.Services.CreateAsyncScope())
            //{
            //    var hangFireService = scope.ServiceProvider.GetRequiredService<INotificationServices>();

            //    RecurringJob.AddOrUpdate(() =>
            //        hangFireService.DeleteLatestOrdersAsync(), Cron.Daily);
            //}

            return app;
        }
    }
}

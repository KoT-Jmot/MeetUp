using Hangfire;
using MeetUp.HangFireSerivce.Application.Contracts;
using MeetUp.HangFireSerivce.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.HangFireSerivce.Api.Extensions
{
    public static class AppExtensions
    {
        public static async Task<WebApplication> ConfigureMigrationAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<HangFireContext>();
                await dbContext.Database.MigrateAsync();
            }

            return app;
        }

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
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var hangFireService = scope.ServiceProvider.GetRequiredService<INotificationServices>();

                RecurringJob.AddOrUpdate(() =>
                    hangFireService.DeleteLatestOrdersAsync(10), Cron.Hourly);
            }

            return app;
        }
    }
}

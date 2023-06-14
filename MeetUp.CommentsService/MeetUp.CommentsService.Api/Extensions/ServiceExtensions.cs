using Mapster;
using MapsterMapper;
using MeetUp.CommentsService.Application.Contracts;
using MeetUp.CommentsService.Infrastructure;
using MeetUp.CommentsService.Infrastructure.Contracts;
using MeetUp.CommentsService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MeetUp.CommentsService.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b =>
                    {
                        b.MigrationsAssembly(Assembly.Load("MeetUp.CommentsService.Infrastructure").FullName);
                    }));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

        public static IServiceCollection ConfigureMapster(
          this IServiceCollection services,
          IConfiguration configuration)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.Load("MeetUp.CommentsService.Application"));

            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }

        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services.AddScoped<ICommentService, Application.Services.CommentService>();

            return services;
        }

    }
}

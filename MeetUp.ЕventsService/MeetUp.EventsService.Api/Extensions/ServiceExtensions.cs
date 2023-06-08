using MeetUp.EventsService.Infrastructure.Repositories;
using MeetUp.EventsService.Infrastructure.Contracts;
using MeetUp.EventsService.Application.Contracts;
using MeetUp.EventsService.Application.Services;
using MeetUp.EventsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MapsterMapper;
using Mapster;
using MeetUp.EventsService.Application.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MeetUp.EventsService.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
                   this IServiceCollection services,
                   IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseLazyLoadingProxies(true)
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
                    {
                        b.MigrationsAssembly(Assembly.Load("MeetUp.EventsService.Infrastructure").FullName);
                    }));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

        public static IServiceCollection ConfigureMapster(
                   this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.Load("MeetUp.EventsService.Application"));

            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }

        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}

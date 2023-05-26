using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.Services;
using MeetUp.IdentityService.Application.Utils;
using MeetUp.IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace MeetUp.IdentityService.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureSqlServer(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
            {
                b.MigrationsAssembly(Assembly.Load("MeetUp.IdentityService.Infrastructure").FullName);
            }));

        return services;
    }

    public static IServiceCollection ConfigureServices(
           this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }

    public static IServiceCollection ConfigureJWT(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        //var secretKey = Environment.GetEnvironmentVariable("SECRET");

        var secretKey = "abama2317";

        services.AddSingleton<JWTConfig>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("validAudience").Value,
                        IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                    };
                });

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<IdentityUser>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;
        });

        IdentityModelEventSource.ShowPII = true;

        builder = new IdentityBuilder(
            builder.UserType,
            typeof(IdentityRole),
            builder.Services);

        builder.AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

        builder.AddRoleManager<RoleManager<IdentityRole>>();

        return services;
    }
}
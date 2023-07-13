using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MeetUp.Gateway.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureJWT(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

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
    }
}

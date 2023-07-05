using Microsoft.Extensions.Configuration;

namespace MeetUp.IdentityService.Application.Utils
{
    public class JWTConfig
    {
        private readonly IConfiguration _configuration;

        public JWTConfig(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("JwtSettings");
        }

        public virtual string GetValidIssuer() => _configuration.GetSection("validIssuer").Value;
        public virtual string GetValidAudience() => _configuration.GetSection("validAudience").Value;
        public virtual double GetExpires() => Convert.ToDouble(_configuration.GetSection("expires").Value);
    }
}

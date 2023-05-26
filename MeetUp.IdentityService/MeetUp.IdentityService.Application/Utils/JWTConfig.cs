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

        public string GetValidIssuer() => _configuration.GetSection("validIssuer").Value;
        public string GetValidAudience() => _configuration.GetSection("validAudience").Value;
        public double GetExpires() => Convert.ToDouble(_configuration.GetSection("expires").Value);
    }
}

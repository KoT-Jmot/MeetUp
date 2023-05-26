using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.Utils;
using MeetUp.IdentityService.Application.Utils.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetUp.IdentityService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _jwtConfig;

        public AccountService(
            UserManager<IdentityUser> userManager,
            JWTConfig jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
        }

        public async Task<string> SignInAsync(
            UserForLoginDto userForLoginDto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);

            if (user is null || !isCorrectPassword)
                throw new LoginUserException();

            return await CreateTokenAsync(user);
        }

        public async Task<string> SignUpAsync(UserForRegistrationDto userForRegistrationDto, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = userForRegistrationDto.UserName,
                PhoneNumber = userForRegistrationDto.PhoneNumber,
                Email = userForRegistrationDto.Email

            };
            var result = await _userManager.CreateAsync(user!, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var message = string.Empty;

                foreach (var error in result.Errors)
                    message += error.Description + "\n";

                throw new RegistrationUserException(message);
            }

            await _userManager.AddToRoleAsync(user, AccountRoles.GetDefaultRole);

            return await CreateTokenAsync(user);
        }

        private async Task<string> CreateTokenAsync(IdentityUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials,
            IEnumerable<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtConfig.GetValidIssuer(),
                audience: _jwtConfig.GetValidAudience(),
                claims: claims,
                expires:
                DateTime.Now.AddMinutes(_jwtConfig.GetExpires()),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}

using MeetUp.IdentityService.Application.Utils.Exceptions;
using MeetUp.IdentityService.Application.RequestFeatures;
using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.Utils;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FluentValidation;
using System.Text;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace MeetUp.IdentityService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _jwtConfig;

        private readonly IValidator<UserForRegistrationDto> _registrationUserValidator;
        private readonly IValidator<UserForLoginDto> _loginUserValidator;

        public AccountService(
            UserManager<IdentityUser> userManager,
            JWTConfig jwtConfig,
            IValidator<UserForRegistrationDto> registrationUserValidator,
            IValidator<UserForLoginDto> loginUserValidator)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
            _registrationUserValidator = registrationUserValidator;
            _loginUserValidator = loginUserValidator;
        }

        public async Task<string> SignInAsync(
            UserForLoginDto userForLoginDto,
            CancellationToken cancellationToken)
        {
            await _loginUserValidator.ValidateAndThrowAsync(userForLoginDto, cancellationToken);

            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);

            if (user is null || !isCorrectPassword)
            {
                throw new LoginUserException();
            }

                return await CreateTokenAsync(user);
        }

        public async Task<string> SignUpAsync(
            UserForRegistrationDto userForRegistrationDto,
            CancellationToken cancellationToken)
        {
            await _registrationUserValidator.ValidateAndThrowAsync(userForRegistrationDto, cancellationToken);

            var user = userForRegistrationDto.Adapt<IdentityUser>();

            var result = await _userManager.CreateAsync(user!, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                var message = string.Empty;

                foreach (var error in result.Errors)
                {
                    message += error.Description + "\n";
                }

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
                new("userIdentifier", user.Id)
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

        public async Task<PagedList<OutputUserDto>> GetAllUsersAsync(UserQueryDto userQuery, CancellationToken cancellationToken)
        {
            var user = _userManager.Users;

            if (!userQuery.UserName.IsNullOrEmpty())
            {
                user = user.Where(u => u.UserName.Contains(userQuery.UserName!));
            }

            user = user.OrderBy(p => p.UserName);
            var totalCount = await user.CountAsync(cancellationToken);

            var pagingUsers = await user
                        .Skip((userQuery.PageNumber - 1) * userQuery.PageSize)
                        .Take(userQuery.PageSize)
                        .ToListAsync(cancellationToken);

            var outputUsers = pagingUsers.Adapt<IEnumerable<OutputUserDto>>();
            var usersWithMetaData = PagedList<OutputUserDto>.ToPagedList(outputUsers, userQuery.PageNumber, totalCount, userQuery.PageSize);

            return usersWithMetaData;
        }

        public async Task<OutputUserDto> GetUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                throw new EntityNotFoundException("User was not found!");
            }

            var outputUser = user.Adapt<OutputUserDto>();

            return outputUser;
        }
    }
}

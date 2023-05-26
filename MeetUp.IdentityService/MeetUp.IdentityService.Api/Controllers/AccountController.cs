using MeetUp.IdentityService.Api.Actions;
using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace MeetUp.IdentityService.Api.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] UserQueryDto userQuery,
            CancellationToken cancellationToken)
        {
            var users = await _accountService.GetAllUsersAsync(userQuery, cancellationToken);

            return new PagingActionResult<PagedList<OutputUserDto>>(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync([FromRoute] string email)
        {
            var users = await _accountService.GetUserByEmail(email);

            return Ok(users);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] UserForRegistrationDto userDto,
            CancellationToken cancellationToken = default)
        {
            var jwtToken = await _accountService.SignUpAsync(userDto, cancellationToken);

            return Created(nameof(SignUpAsync), jwtToken);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(
            [FromBody] UserForLoginDto userDto,
            CancellationToken cancellationToken)
        {
            var jwtToken = await _accountService.SignInAsync(userDto, cancellationToken);

            return Ok(jwtToken);
        }
    }
}

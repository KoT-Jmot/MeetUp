using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.DTOs.InputDto;
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

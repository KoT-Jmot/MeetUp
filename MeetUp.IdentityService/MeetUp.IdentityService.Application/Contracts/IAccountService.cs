using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.OutputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.RequestFeatures;

namespace MeetUp.IdentityService.Application.Contracts
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(
            UserForRegistrationDto userForRegistrationDto,
            CancellationToken cancellationToken);

        Task<string> SignInAsync(
            UserForLoginDto userForLoginDto,
            CancellationToken cancellationToken);

        Task<PagedList<OutputUserDto>> GetAllUsersAsync(
            UserQueryDto userQuery,
            CancellationToken cancellationToken);
        Task<OutputUserDto> GetUserByEmail(string userEmail);
    }
}

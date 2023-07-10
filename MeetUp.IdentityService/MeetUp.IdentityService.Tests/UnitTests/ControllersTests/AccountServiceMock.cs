using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.Contracts;
using Moq;

namespace MeetUp.IdentityService.Tests.UnitTests.ControllersTests
{
    public static class AccountServiceMock
    {
        public static Mock<IAccountService> Create()
        {
            var accountService = new Mock<IAccountService>();

            accountService.Setup(r => r.SignUpAsync(
                           It.IsAny<UserForRegistrationDto>(),
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Guid.NewGuid().ToString());

            accountService.Setup(r => r.SignInAsync(
                           It.IsAny<UserForLoginDto>(),
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(Guid.NewGuid().ToString());

            accountService.Setup(r => r.GetUserByEmail(
                           It.IsAny<string>()))
                          .ReturnsAsync(DataFactory.GetOutputUserDto());

            accountService.Setup(r => r.GetAllUsersAsync(
                           It.IsAny<UserQueryDto>(),
                           It.IsAny<CancellationToken>()))
                          .ReturnsAsync(DataFactory.GetOutputUsers());

            return accountService;
        }
    }
}

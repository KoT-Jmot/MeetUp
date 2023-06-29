using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Api.Controllers;
using Moq;

namespace MeetUp.IdentityService.Tests.UnitTests.ControllersTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountService;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _accountService = new Mock<IAccountService>();

            _accountService.Setup(r => r.SignUpAsync(It.IsAny<UserForRegistrationDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid().ToString());
            _accountService.Setup(r => r.SignInAsync(It.IsAny<UserForLoginDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid().ToString());
            _accountService.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(DataFactory.GetOutputUserDto());
            _accountService.Setup(r => r.GetAllUsersAsync(It.IsAny<UserQueryDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(DataFactory.GetAllOutputUsersDto());

            _accountController = new AccountController(_accountService.Object);
        }

        [Fact]
        public async Task SignInAsync_WithCorrectData_ShouldReturnStatusCode200WithData()
        {
            //Arrange
            var userDto = DataFactory.GetUserForLoginDto();

            //Act
            var result = await _accountController.SignInAsync(userDto, default);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SignUpAsync_WithCorrectData_ShouldReturnStatusCodeCreatedWithData()
        {
            //Arrange
            var userDto = DataFactory.GetUserForRegistrationDto();

            //Act
            var result = await _accountController.SignUpAsync(userDto, default);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WithCorrectData_ShouldReturnStatusCode200WithData()
        {
            //Arrange
            var userEmail = DataFactory.GetOutputUserDto().Email;

            //Act
            var result = await _accountController.GetUserByEmailAsync(userEmail);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithCorrectData_ShouldReturnStatusCode200WithData()
        {
            //Arrange
            var userQuery = new UserQueryDto();

            //Act
            var result = await _accountController.GetAllUsersAsync(userQuery, default);

            //Assert
            Assert.NotNull(result);
        }
    }
}

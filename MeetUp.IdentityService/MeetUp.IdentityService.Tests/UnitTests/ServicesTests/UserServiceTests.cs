using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.Services;
using MeetUp.IdentityService.Application.Utils;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Moq;
using Microsoft.Extensions.Configuration;
using MeetUp.IdentityService.Application.Utils.Exceptions;

namespace MeetUp.IdentityService.Tests.UnitTests.ServicesTests
{
    public class UserServiceTests
    {
        private readonly Mock<IValidator<UserForRegistrationDto>> _registrationUserValidatorMock;
        private readonly Mock<IValidator<UserForLoginDto>> _loginUserValidatorMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<JWTConfig> _jwtConfigMock;
        private readonly Mock<IConfiguration> _configuration;
        private readonly IAccountService _userService;

        public UserServiceTests()
        {
            _registrationUserValidatorMock = new Mock<IValidator<UserForRegistrationDto>>();
            _loginUserValidatorMock = new Mock<IValidator<UserForLoginDto>>();
            _userManagerMock = CreateRepositoryManagerMock();
            _configuration = new Mock<IConfiguration>();
            _jwtConfigMock = CreateJwtConfigMock(_configuration.Object);

            Environment.SetEnvironmentVariable("SECRET", "MeetUpIdentityService");

            _userService = new AccountService(_userManagerMock.Object, _jwtConfigMock.Object, _registrationUserValidatorMock.Object, _loginUserValidatorMock.Object);
        }

        private Mock<UserManager<IdentityUser>> CreateRepositoryManagerMock()
        {
            var repositoryManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

            repositoryManager.Setup(r => r.AddToRoleAsync(It.IsAny<IdentityUser>(), AccountRoles.GetDefaultRole)).ReturnsAsync(IdentityResult.Success);
            repositoryManager.Setup(r => r.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "user" });

            return repositoryManager;
        }

        private Mock<JWTConfig> CreateJwtConfigMock(IConfiguration configuration)
        {
            var jwtConfig = new Mock<JWTConfig>(configuration);

            jwtConfig.Setup(r => r.GetValidIssuer()).Returns("MeetUp");
            jwtConfig.Setup(r => r.GetValidAudience()).Returns("https://localhost:5001");
            jwtConfig.Setup(r => r.GetExpires()).Returns(30);

            return jwtConfig;
        }

        [Fact]
        public async Task RegisterUser_WhenUserDoNotExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = ServicesDataFactory.GetUserForRegistrationDto();

            _userManagerMock.Setup(r => r.CreateAsync(It.IsAny<IdentityUser>(), userDto.Password)).ReturnsAsync(IdentityResult.Success);

            //Act
            var result = await _userService.SignUpAsync(userDto, default);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task RegisterUser_WhenUserExists_ShouldReturnRegistrationUserException()
        {
            //Arrange
            var userDto = ServicesDataFactory.GetUserForRegistrationDto();

            _userManagerMock.Setup(r => r.CreateAsync(It.IsAny<IdentityUser>(), userDto.Password)).ReturnsAsync(IdentityResult.Failed());

            //Act
            var signUpProcess = _userService.SignUpAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<RegistrationUserException>(async () => await signUpProcess);
        }

        [Fact]
        public async Task LoginUser_WhenUserExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = ServicesDataFactory.GetUserForLoginDto();
            var user = ServicesDataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(r => r.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(true);

            //Act
            var result = await _userService.SignInAsync(userDto, default);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task LoginUser_WhenUserDoNotExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = ServicesDataFactory.GetUserForLoginDto();
            var user = ServicesDataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(default(IdentityUser));
            _userManagerMock.Setup(r => r.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(true);

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async ()=> await signInProcess);
        }

        [Fact]
        public async Task LoginUser_WithIncorrectPassword_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = ServicesDataFactory.GetUserForLoginDto();
            var user = ServicesDataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(r => r.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(false);

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async () => await signInProcess);
        }
    }
}

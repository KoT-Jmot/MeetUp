using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.Services;
using MeetUp.IdentityService.Application.Utils;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Moq;
using Microsoft.Extensions.Configuration;
using MeetUp.IdentityService.Application.Utils.Exceptions;
using MeetUp.IdentityService.Application.DTOs.QueryDto;

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
            _configuration = new Mock<IConfiguration>();
            _jwtConfigMock = JwtConfigMock.Create(_configuration.Object);
            _userManagerMock = RepositoryManagerMock.Create();

            Environment.SetEnvironmentVariable("SECRET", "MeetUpIdentityService");

            _userService = new AccountService(_userManagerMock.Object, _jwtConfigMock.Object, _registrationUserValidatorMock.Object, _loginUserValidatorMock.Object);
        }

        [Fact]
        public async Task RegisterUser_WhenUserDoNotExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = DataFactory.GetUserForRegistrationDto();

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
            var userDto = DataFactory.GetUserForRegistrationDto();

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
            var userDto = DataFactory.GetUserForLoginDto();
            var user = DataFactory.GetUserEntity();

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
            var userDto = DataFactory.GetUserForLoginDto();
            var user = DataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(default(IdentityUser));
            _userManagerMock.Setup(r => r.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(true);

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async () => await signInProcess);
        }

        [Fact]
        public async Task LoginUser_WithIncorrectPassword_ShouldReturnSuccessResult()
        {
            //Arrange
            var userDto = DataFactory.GetUserForLoginDto();
            var user = DataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(r => r.CheckPasswordAsync(user, userDto.Password)).ReturnsAsync(false);

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async () => await signInProcess);
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserExists_ShouldReturnUser()
        {
            //Arrange
            var user = DataFactory.GetUserEntity();
            var userEmail = user.Email;
            var outputUser = DataFactory.GetOutputUserDto();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userEmail)).ReturnsAsync(user);

            //Act
            var result = await _userService.GetUserByEmail(userEmail);

            //Assert
            Assert.Equal(outputUser.Id, result.Id);
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserDoNotExists_ShouldReturnEntityNotFoundException()
        {
            //Arrange
            var userEmail = DataFactory.GetUserEntity().Email;

            _userManagerMock.Setup(r => r.FindByEmailAsync(userEmail)).ReturnsAsync(default(IdentityUser));

            //Act
            var getUserByEmailProcess = _userService.GetUserByEmail(userEmail);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await getUserByEmailProcess);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithPaging_ShouldReturnAllUsers()
        {
            //Arrange
            var userQuery = new UserQueryDto();
            var outputUserCount = 3;

            //Act
            var result = await _userService.GetAllUsersAsync(userQuery, default);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(outputUserCount, result.Count);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithPagingByUserName_ShouldReturnAllUsers()
        {
            //Arrange
            var userQuery = new UserQueryDto()
            {
                UserName = "i"
            };
            var outputUserCount = 2;

            //Act
            var result = await _userService.GetAllUsersAsync(userQuery, default);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(outputUserCount, result.Count);
        }
    }
}

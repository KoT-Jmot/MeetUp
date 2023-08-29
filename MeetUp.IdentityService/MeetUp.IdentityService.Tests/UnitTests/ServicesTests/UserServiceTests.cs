using MeetUp.IdentityService.Application.Utils.Exceptions;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.DTOs.QueryDto;
using MeetUp.IdentityService.Application.Contracts;
using MeetUp.IdentityService.Application.Services;
using MeetUp.IdentityService.Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MeetUp.IdentityService.Tests.UnitTests.ServicesTests
{
    public class UserServiceTests
    {
        private readonly Mock<IValidator<UserForRegistrationDto>> _registrationUserValidatorMock;
        private readonly Mock<IValidator<UserForLoginDto>> _loginUserValidatorMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<JWTConfig> _jwtConfigMock;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<ICacheService> _cacheService;
        private readonly IAccountService _userService;

        public UserServiceTests()
        {
            _registrationUserValidatorMock = new Mock<IValidator<UserForRegistrationDto>>();
            _loginUserValidatorMock = new Mock<IValidator<UserForLoginDto>>();
            _configuration = new Mock<IConfiguration>();
            _jwtConfigMock = JwtConfigMock.Create(_configuration.Object);
            _userManagerMock = RepositoryManagerMock.Create();
            _cacheService = new Mock<ICacheService>();

            Environment.SetEnvironmentVariable("SECRET", "MeetUpIdentityService");

            _userService = new AccountService(_userManagerMock.Object, _jwtConfigMock.Object,_registrationUserValidatorMock.Object, _loginUserValidatorMock.Object, _cacheService.Object);
        }

        [Fact]
        public async Task RegisterUser_WhenUserDoesNotExist_ShouldReturnJwtTocken()
        {
            //Arrange
            var userDto = DataFactory.GetUserForRegistrationDto();

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

            _userManagerMock.Setup(r => r.CreateAsync(
                             It.IsAny<IdentityUser>(),
                             userDto.Password))
                            .ReturnsAsync(IdentityResult.Failed());

            //Act
            var signUpProcess = _userService.SignUpAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<RegistrationUserException>(async () => await signUpProcess);
        }

        [Fact]
        public async Task LoginUser_WhenUserExists_ShouldReturnJwtTocken()
        {
            //Arrange
            var userDto = DataFactory.GetUserForLoginDto();

            //Act
            var result = await _userService.SignInAsync(userDto, default);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task LoginUser_WhenUserDoesNotExist_ShouldReturnLoginUserException()
        {
            //Arrange
            var userDto = DataFactory.GetUserForLoginDto();
            var user = DataFactory.GetUserEntity();

            _userManagerMock.Setup(r => r.FindByEmailAsync(userDto.Email)).ReturnsAsync(default(IdentityUser));

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async () => await signInProcess);
        }

        [Fact]
        public async Task LoginUser_WithIncorrectPassword_ShouldReturnLoginUserException()
        {
            //Arrange
            var userDto = DataFactory.GetUserForLoginDto();

            _userManagerMock.Setup(r => r.CheckPasswordAsync(
                             It.IsAny<IdentityUser>(),
                             userDto.Password))
                            .ReturnsAsync(false);

            //Act
            var signInProcess = _userService.SignInAsync(userDto, default);

            //Assert
            await Assert.ThrowsAsync<LoginUserException>(async () => await signInProcess);
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserExists_ShouldReturnUser()
        {
            //Arrange
            var userEmail = DataFactory.GetUserEntity().Email;
            var outputUser = DataFactory.GetOutputUserDto();

            //Act
            var result = await _userService.GetUserByEmail(userEmail);

            //Assert
            Assert.Equal(outputUser.Id, result.Id);
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserDoesNotExist_ShouldReturnEntityNotFoundException()
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

            //Act
            var result = await _userService.GetAllUsersAsync(userQuery, default);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count, result.MetaData.TotalCount);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithPagingByUserName_ShouldReturnAllUsers()
        {
            //Arrange
            var userQuery = new UserQueryDto()
            {
                UserName = "i"
            };

            //Act
            var result = await _userService.GetAllUsersAsync(userQuery, default);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count, result.MetaData.TotalCount);
        }
    }
}

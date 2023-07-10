using FluentValidation;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.Validation;

namespace MeetUp.IdentityService.Tests.UnitTests.ValidationTests
{
    public class LoginUserValidatorTests
    {
        private readonly IValidator<UserForLoginDto> _loginValidator;

        public LoginUserValidatorTests()
        {
            _loginValidator = new LoginUserValidator();
        }

        [Theory]
        [InlineData("", "aaa111aaa", false)]
        [InlineData("test", "aaa111aaa", false)]
        [InlineData("test@gmail.com", "a1a", false)]
        [InlineData("test@gmail.com","aaa111aaa", true)]
        [InlineData("test@gmail.com", "aaaaaaa1", true)]
        [InlineData("test@gmail.com", "aaaaaaaa", false)]
        [InlineData("test@gmail.com", "11111111", false)]
        [InlineData("test@gmail.com", "jnjoxzdgcbqtiiejtklefhdbcwpgrgmsmlzkhbzxgmwfrseld11", false)] // 51 symbol in password
        public void UserForLoginDtoValidatorTests(
            string email,
            string password,
            bool isValid)
        {
            var _userLoginDto = new UserForLoginDto
            {
                Email = email,
                Password = password
            };

            Assert.Equal(isValid, _loginValidator.Validate(_userLoginDto).IsValid);
        }
    }
}

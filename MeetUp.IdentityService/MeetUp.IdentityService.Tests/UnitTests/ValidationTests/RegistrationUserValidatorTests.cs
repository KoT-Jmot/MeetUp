using FluentValidation;
using MeetUp.IdentityService.Application.DTOs.InputDto;
using MeetUp.IdentityService.Application.Validation;

namespace MeetUp.IdentityService.Tests.UnitTests.ValidationTests
{
    public class RegistrationUserValidatorTests
    {
        private readonly IValidator<UserForRegistrationDto> _regValidator;

        public RegistrationUserValidatorTests()
        {
            _regValidator = new RegistrationUserValidator();
        }

        [Theory]
        [InlineData("ivan","test@gmail.com","+375192048276", "aaa111aaa", true)]
        [InlineData("", "test@gmail.com", "+375192048276", "aaa111aaa", false)]
        [InlineData("ankaiprjwhljxzcofvorhqqdqyurqaa", "test@gmail.com", "+375192048276", "aaa111aaa", false)] // 31 symbol in username
        [InlineData("ivan123", "test@gmail.com", "+375192048276", "aaa111aaa", true)]
        [InlineData("ivan", "", "+375192048276", "aaa111aaa", false)]
        [InlineData("ivan", "test", "+375192048276", "aaa111aaa", false)]
        [InlineData("ivan", "test123@gmail.com", "+375192048276", "aaa111aaa", true)]
        [InlineData("ivan", "test@gmail.com", "", "aaa111aaa", false)]
        [InlineData("ivan", "test@gmail.com", "2175192048276", "aaa111aaa", false)]
        [InlineData("ivan", "test@gmail.com", "123", "aaa111aaa", false)]
        [InlineData("ivan", "test@gmail.com", "375192048276", "aaa111aaa", false)]
        [InlineData("ivan", "test@gmail.com", "+3751920482", "aaa111aaa", true)]
        [InlineData("ivan", "test@gmail.com", "+37519a048276", "aaa111aaa", false)]
        [InlineData("ivan", "test@gmail.com", "+375192048276", "", false)]
        [InlineData("ivan", "test@gmail.com", "+375192048276", "aaaaaaaaa", false)]
        [InlineData("ivan", "test@gmail.com", "+375192048276", "111111111", false)]
        [InlineData("ivan", "test@gmail.com", "+375192048276", "a1a", false)]
        [InlineData("ivan", "test@gmail.com", "+375192048276", "jnjoxzdgcbqtiiejtklefhdbcwpgrgmsmlzkhbzxgmwfrseld11", false)] // 51 symbol in password
        public void UserForRegDtoValidatorTests(
            string username,
            string email,
            string phonenumber,
            string password,
            bool isValid)
        {
            var _userRegDto = new UserForRegistrationDto
            {
                UserName = username,
                Email = email,
                PhoneNumber = phonenumber,
                Password = password
            };

            Assert.Equal(isValid, _regValidator.Validate(_userRegDto).IsValid);
        }
    }
}

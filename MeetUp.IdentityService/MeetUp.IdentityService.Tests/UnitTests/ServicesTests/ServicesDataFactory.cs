using MeetUp.IdentityService.Application.DTOs.InputDto;
using Microsoft.AspNetCore.Identity;

namespace MeetUp.IdentityService.Tests.UnitTests.ServicesTests
{
    public static class ServicesDataFactory
    {
        public static UserForRegistrationDto GetUserForRegistrationDto()
        {
            return new UserForRegistrationDto
            {
                UserName = "ivan",
                Email = "test@gmail.com",
                PhoneNumber = "+375112345781",
                Password = "aaa111aaa"
            };
        }
        public static UserForLoginDto GetUserForLoginDto()
        {
            return new UserForLoginDto
            {
                Email = "test@gmail.com",
                Password = "aaa111aaa"
            };
        }

        public static IdentityUser GetUserEntity()
        {
            return new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "ivan",
                Email = "test@gmail.com",
                PhoneNumber = "+375112345781"
            };
        }
    }
}

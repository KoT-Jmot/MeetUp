using Microsoft.AspNetCore.Identity;

namespace MeetUp.IdentityService.Tests.IntegrationTests
{
    public static class DataFactory
    {
        public static string UserPassword = "aaa111aaa";

        public static IEnumerable<IdentityUser> GetAllUsersEntity()
        {
            return new List<IdentityUser>
            {
                new IdentityUser()
                {
                    Id = "21f85435-204d-4e1f-80bb-08db734a088b",
                    UserName = "ivan",
                    Email = "test@gmail.com",
                    PhoneNumber = "+375112345781"
                },
                new IdentityUser()
                {
                    Id = "52fc493e-ed46-4df7-5544-08db734a420d",
                    UserName = "vlad",
                    Email = "vlad@gmail.com",
                    PhoneNumber = "+375295712360"
                },
                new IdentityUser()
                {
                    Id = "c7264143-e47a-42e4-b97a-29d02088282a",
                    UserName = "kirill",
                    Email = "kirill@gmail.com",
                    PhoneNumber = "+375103912871"
                }
            };
        }
    }
}

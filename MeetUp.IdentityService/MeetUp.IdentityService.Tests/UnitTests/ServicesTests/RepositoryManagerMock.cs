using MeetUp.IdentityService.Application.Utils;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;

namespace MeetUp.IdentityService.Tests.UnitTests.ServicesTests
{
    public static class RepositoryManagerMock
    {
        public static Mock<UserManager<IdentityUser>> Create()
        {
            var repositoryManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

            repositoryManager.Setup(r => r.AddToRoleAsync(It.IsAny<IdentityUser>(), AccountRoles.GetDefaultRole)).ReturnsAsync(IdentityResult.Success);
            repositoryManager.Setup(r => r.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            repositoryManager.Setup(r => r.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { AccountRoles.GetDefaultRole });
            repositoryManager.Setup(r => r.Users).Returns(DataFactory.GetAllUsersEntity().BuildMock());

            return repositoryManager;
        }
    }
}

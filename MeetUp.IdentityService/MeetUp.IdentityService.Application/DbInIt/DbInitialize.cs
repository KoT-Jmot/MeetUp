using MeetUp.IdentityService.Application.Utils;
using Microsoft.AspNetCore.Identity;

namespace MeetUp.IdentityService.Application.DbInIt
{
    public static class DbInitialize
    {
        public static async Task RolesInitialize(this RoleManager<IdentityRole> roleManager)
        {
            var userName = AccountRoles.GetDefaultRole;
            var userIsInitialize = await roleManager.RoleExistsAsync(userName);

            if (!userIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(userName));
        }
    }
}

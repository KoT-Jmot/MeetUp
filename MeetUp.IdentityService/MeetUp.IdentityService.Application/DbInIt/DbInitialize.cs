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

            var adminName = AccountRoles.GetAdministratorRole;
            var adminIsInitialize = await roleManager.RoleExistsAsync(adminName);

            if (!adminIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(adminName));

            var superAdminName = AccountRoles.GetSuperAdministratorRole;
            var superAdminIsInitialize = await roleManager.RoleExistsAsync(superAdminName);

            if (!superAdminIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(superAdminName));

            var blockedRole = AccountRoles.GetBlockedRole;
            var blockedIsInitialize = await roleManager.RoleExistsAsync(blockedRole);

            if (!blockedIsInitialize)
                await roleManager.CreateAsync(new IdentityRole(blockedRole));
        }
    }
}

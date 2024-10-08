﻿using Microsoft.AspNetCore.Identity;
using TechWave.Models.DomainModel;

namespace TechWave.Models
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserAsync(
IServiceProvider provider)
        {
            var roleManager =
            provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager =
            provider.GetRequiredService<UserManager<User>>();
            string username = "Admin";
            string email = "admin@gmail.com";
            string password = "Admin@123";
            string roleName = "Admin";
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username, Email = email };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}

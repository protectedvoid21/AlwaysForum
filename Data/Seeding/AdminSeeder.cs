using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Seeding; 

public class AdminSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        var userManager = serviceProvider.GetRequiredService<UserManager<ForumUser>>();

        bool adminExists = await userManager.Users.AnyAsync(u => u.UserName == GlobalConstants.AdminUserName);
        if (adminExists) {
            return;
        }

        ForumUser adminUser = new() {
            UserName = GlobalConstants.AdminUserName,
            Email = GlobalConstants.AdminEmail,
            EmailConfirmed = true,
        };
        IdentityResult result = await userManager.CreateAsync(adminUser, GlobalConstants.AdminPassword);
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (result.Succeeded) {
            var isRoleExisting = await roleManager.RoleExistsAsync(GlobalConstants.AdminRoleName);
            if (!isRoleExisting) {
                throw new Exception("Missing admin role");
            }

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdminRoleName);
        }
    }
}
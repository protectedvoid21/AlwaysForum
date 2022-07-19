using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Seeding; 

public class UserSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        var userManager = serviceProvider.GetRequiredService<UserManager<ForumUser>>();

        bool adminExists = await userManager.Users.AnyAsync(u => u.UserName == GlobalConstants.AdminUserName);
        if (adminExists) {
            ForumUser adminUser = new() {
                UserName = GlobalConstants.AdminUserName,
            };
            await userManager.CreateAsync(adminUser, GlobalConstants.AdminPassword);
        }
    }
}
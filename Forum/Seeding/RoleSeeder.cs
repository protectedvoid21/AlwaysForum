using Data;
using Microsoft.AspNetCore.Identity;

namespace AlwaysForum.Seeding;

internal class RoleSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) { 
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if(!await roleManager.RoleExistsAsync(GlobalConstants.AdminRoleName)) {
            await roleManager.CreateAsync(new IdentityRole { Name = GlobalConstants.AdminRoleName });
        }

        foreach (var roleName in GlobalConstants.RequiredRoles) {
            if (!await roleManager.RoleExistsAsync(roleName)) {
                await roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }
    }
}
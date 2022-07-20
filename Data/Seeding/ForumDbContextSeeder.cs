using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seeding;

public class ForumDbContextSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        if (dbContext == null) {
            throw new ArgumentNullException(nameof(dbContext));
        }

        if (serviceProvider == null) {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        var seeders = new List<ISeeder> {
            new RoleSeeder(),
            new AdminSeeder(),
        };

        foreach (var seeder in seeders) {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();
        }
    }
}
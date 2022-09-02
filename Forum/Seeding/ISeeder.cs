using Data;

namespace AlwaysForum.Seeding;

internal interface ISeeder {
    Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider);
}
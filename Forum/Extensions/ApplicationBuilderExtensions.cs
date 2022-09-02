using AlwaysForum.Seeding;
using Data;

namespace AlwaysForum.Extensions;

public static class ApplicationBuilderExtensions {
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app) {
        using var serviceScope = app.ApplicationServices.CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ForumDbContext>();

        new ForumDbContextSeeder()
            .SeedAsync(dbContext, serviceScope.ServiceProvider)
            .GetAwaiter()
            .GetResult();

        return app;
    }
}
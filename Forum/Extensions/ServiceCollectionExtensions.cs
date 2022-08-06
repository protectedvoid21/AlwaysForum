using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Services.Posts;
using Services.Sections;

namespace AlwaysForum.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection) {
        serviceCollection.AddIdentity<ForumUser, IdentityRole>(options => {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ForumDbContext>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection) {
        return serviceCollection
            .AddTransient<ISectionsService, SectionsService>()
            .AddTransient<IPostsService, PostsService>();
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Models.Extensions; 

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection) {
        serviceCollection.AddIdentity<ForumUser, IdentityRole>(options => {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ForumDbContext>();
        return serviceCollection;
    }
}
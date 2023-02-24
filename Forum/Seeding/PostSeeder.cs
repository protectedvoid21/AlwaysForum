using Data;
using Services.Posts;

namespace AlwaysForum.Seeding; 

public class PostSeeder : ISeeder {
    public Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        //var postService = serviceProvider.GetRequiredService<IPostsService>();
        throw new NotImplementedException();
    }
}

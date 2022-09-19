using Data;
using Services.Tags;

namespace AlwaysForum.Seeding; 

public class TagSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        ITagsService tagsService = serviceProvider.GetRequiredService<ITagsService>();

        string[] tags = {
            "lifestyle", "help", "hobby", "sport", "school", "programming", "health", "politics", "history",
            "cosmetics", "advice", "controversial", "music", "art", "gaming", "announcement", "movies", "serials",
            "anime"
        };

        foreach (var tagName in tags) {
            await tagsService.AddAsync(tagName);
        }
    }
}
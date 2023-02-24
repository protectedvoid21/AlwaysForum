using Data;
using Data.Models;
using Services.Sections;

namespace AlwaysForum.Seeding;

public class SectionSeeder : ISeeder {
    public async Task SeedAsync(ForumDbContext dbContext, IServiceProvider serviceProvider) {
        var sectionsService = serviceProvider.GetRequiredService<ISectionsService>();

        await sectionsService.AddAsync("Ideas", "Share your ideas about forum and potential changes");
        await sectionsService.AddAsync("Help", "Section for new users who are in need of help");
        await sectionsService.AddAsync("Opinions", "Tell everyone your opinion on ongoing events");
    }
}

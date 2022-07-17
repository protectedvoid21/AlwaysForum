using AlwaysForum.Models;

namespace AlwaysForum.Services.Sections;

public class SectionsService : ISectionsService {
    private readonly ForumDbContext dbContext;
    
    public SectionsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string name) {
        Section section = new() {
            Name = name,
        };
        await dbContext.AddAsync(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Section>> GetAll() {
        return dbContext.Sections;
    }

    public async Task UpdateAsync(int id, string name) {
        var section = await dbContext.Sections.FindAsync(id);
        section.Name = name;

        dbContext.Update(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        var section = await dbContext.Sections.FindAsync(id);
        
        dbContext.Remove(section);
        await dbContext.SaveChangesAsync();
    }
}
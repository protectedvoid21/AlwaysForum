using Data;
using Data.Models;

namespace Services.Sections;

public class SectionsService : ISectionsService {
    private readonly ForumDbContext dbContext;
    
    public SectionsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string name, string description) {
        Section section = new() {
            Name = name,
            Description = description
        };
        await dbContext.AddAsync(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Section> GetById(int id) {
        Section section = await dbContext.Sections.FindAsync(id);
        return section;
    }

    public async Task<IEnumerable<Section>> GetAll() {
        return dbContext.Sections;
    }

    public async Task UpdateAsync(int id, string name, string description) {
        var section = await dbContext.Sections.FindAsync(id);
        section.Name = name;
        section.Description = description;

        dbContext.Update(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        var section = await dbContext.Sections.FindAsync(id);
        
        dbContext.Remove(section);
        await dbContext.SaveChangesAsync();
    }
}
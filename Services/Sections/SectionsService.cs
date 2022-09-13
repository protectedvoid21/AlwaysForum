using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Sections;

public class SectionsService : ISectionsService {
    private readonly ForumDbContext dbContext;
    private readonly IMapper mapper;
    
    public SectionsService(ForumDbContext dbContext, IMapper mapper) {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task AddAsync(string name, string description) {
        Section section = new() {
            Name = name,
            Description = description
        };
        await dbContext.AddAsync(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task<TSection> GetById<TSection>(int id) {
        return await dbContext.Sections
            .Where(s => s.Id == id)
            .ProjectTo<TSection>(mapper.ConfigurationProvider)
            .FirstAsync();
    }

    public async Task<IEnumerable<Section>> GetAll() {
        return dbContext.Sections
            .Include(s => s.Posts);
    }

    public async Task<int> GetPostCount(int id) {
        return await dbContext.Posts.Where(p => p.SectionId == id).CountAsync();
    }

    public async Task UpdateAsync(int id, string name, string description) {
        var section = await dbContext.Sections.FindAsync(id);
        section.Name = name;
        section.Description = description;

        dbContext.Update(section);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        Section section = await dbContext.Sections
            .Include(s => s.Posts)
            .FirstAsync(s => s.Id == id);

        dbContext.Remove(section);
        await dbContext.SaveChangesAsync();
    }
}
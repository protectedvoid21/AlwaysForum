using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.PostReports; 

public class PostReportsService : IPostReportsService {
    private readonly ForumDbContext dbContext;
    private readonly IMapper mapper;

    public PostReportsService(ForumDbContext dbContext, IMapper mapper) {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task AddAsync(int postId, string authorId, int reportTypeId, string description) {
        if (await dbContext.PostReports.FirstOrDefaultAsync(r => r.AuthorId == authorId && r.PostId == postId) != null) {
            return;
        }

        PostReport postReport = new() {
            PostId = postId,
            Description = description,
            AuthorId = authorId,
            ReportTypeId = reportTypeId,
            CreateDate = DateTime.Now,
        };

        await dbContext.AddAsync(postReport);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TModel>> GetAll<TModel>() {
        return await dbContext.PostReports
            .ProjectTo<TModel>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id) {
        PostReport? postReport = await dbContext.PostReports.FindAsync(id);
        if (postReport == null) {
            return;
        }

        dbContext.Remove(postReport);
        await dbContext.SaveChangesAsync();
    }
}
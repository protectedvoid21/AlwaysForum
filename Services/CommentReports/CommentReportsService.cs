using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.CommentReports; 

public class CommentReportsService : ICommentReportsService {
    private readonly ForumDbContext dbContext;

    public CommentReportsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(int commentId, string authorId, int reportTypeId, string description) {
        if(await dbContext.CommentReports.FirstOrDefaultAsync(r => r.AuthorId == authorId && r.CommentId == commentId) != null) {
            return;
        }

        CommentReport commentReport = new() {
            CommentId = commentId,
            Description = description,
            AuthorId = authorId,
            ReportTypeId = reportTypeId,
            CreateDate = DateTime.Now,
        };

        await dbContext.AddAsync(commentReport);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentReport>> GetAll() {
        return dbContext.CommentReports;
    }

    public async Task DeleteAsync(int id) {
        CommentReport? commentReport = await dbContext.CommentReports.FindAsync(id);
        if(commentReport == null) {
            return;
        }

        dbContext.Remove(commentReport);
        await dbContext.SaveChangesAsync();
    }
}
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Comments;

public class CommentsService : ICommentsService {
    private readonly ForumDbContext dbContext;

    public CommentsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(string description, int postId, string authorId) {
        Comment comment = new() {
            Description = description,
            PostId = postId,
            AuthorId = authorId,
            CreatedTime = DateTime.Now
        };
        await dbContext.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>> GetByPost(int postId) {
        return dbContext.Comments
            .Include(c => c.Author)
            .Where(c => c.PostId == postId);
    }

    public async Task<int> GetCountInPost(int postId) {
        return await dbContext.Comments.CountAsync(p => p.PostId == postId);
    }

    public async Task UpdateAsync(int id, string description) {
        Comment? comment = await dbContext.Comments.FindAsync(id);
        if (comment == null) {
            return;
        }
        comment.Description = description;

        dbContext.Update(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        Comment? comment = await dbContext.Comments.FindAsync(id);
        if (comment == null) {
            return;
        }

        dbContext.Remove(comment);
        await dbContext.SaveChangesAsync();
    }
}
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Posts; 

public class PostsService : IPostsService {
    private readonly ForumDbContext dbContext;

    public PostsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task<Post> GetById(int id) {
        Post post = await dbContext.Posts
            .Include(p => p.Author)
            .FirstAsync(p => p.Id == id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetBySection(int sectionId) {
        return dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == sectionId);
    }

    public async Task<int> AddAsync(string title, string description, string authorId, int sectionId) {
        Post post = new() {
            Title = title,
            Description = description,
            AuthorId = authorId,
            SectionId = sectionId,
            CreatedDate = DateTime.Now,
        };
        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        return post.Id;
    }

    public async Task<bool> IsAuthor(int postId, string authorId) {
        Post post = await dbContext.Posts.FindAsync(postId);
        if (post == null) {
            return false;
        }
        return post.AuthorId == authorId;
    }

    public async Task UpdateAsync(int id, string title, string description) {
        Post post = await dbContext.Posts.FindAsync(id);
        post.Title = title;
        post.Description = description;

        dbContext.Update(post);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        Post post = await dbContext.Posts.FindAsync(id);
        dbContext.Remove(post);
        await dbContext.SaveChangesAsync();
    }
}
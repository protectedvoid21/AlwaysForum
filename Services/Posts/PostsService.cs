using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Tags;

namespace Services.Posts; 

public class PostsService : IPostsService {
    private readonly ForumDbContext dbContext;
    private readonly ITagsService tagsService;

    public PostsService(ForumDbContext dbContext, ITagsService tagsService) {
        this.dbContext = dbContext;
        this.tagsService = tagsService;
    }

    public async Task<Post> GetById(int id) {
        Post post = await dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .ThenInclude(pt => pt.Tag)
            .FirstAsync(p => p.Id == id);
        return post;
    }

    public async Task<IEnumerable<Post>> GetBySection(int sectionId) {
        return dbContext.Posts
            .Include(p => p.Author)
            .Where(p => p.SectionId == sectionId);
    }

    public async Task<int> GetCommentCount(int id) {
        return await dbContext.Comments.Where(c => c.PostId == id).CountAsync();
    }

    public async Task<int> AddAsync(string title, string description, string authorId, int sectionId, IEnumerable<int> tagIds) {
        Post post = new() {
            Title = title,
            Description = description,
            AuthorId = authorId,
            SectionId = sectionId,
            CreatedDate = DateTime.Now,
        };

        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        tagIds = tagIds.Take(GlobalConstants.MaxTagsOnPost);
        foreach(var tagId in tagIds) {
            await tagsService.AddToPost(tagId, post.Id);
        }

        return post.Id;
    }

    public async Task<bool> IsAuthor(int postId, string authorId) {
        Post? post = await dbContext.Posts.FindAsync(postId);
        if (post == null) {
            return false;
        }
        return post.AuthorId == authorId;
    }

    public async Task UpdateAsync(int id, string title, string description, IEnumerable<int> tagIds) {
        Post? post = await dbContext.Posts.FindAsync(id);
        if (post == null) {
            return;
        }
        post.Title = title;
        post.Description = description;

        tagIds = tagIds.Take(GlobalConstants.MaxTagsOnPost);
        await tagsService.UpdateTagsOnPost(id, tagIds);

        dbContext.Update(post);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        Post? post = await dbContext.Posts
            .Include(p => p.Comments)
            .Include(p => p.PostReports)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (post == null) {
            return;
        }

        dbContext.Remove(post);
        await dbContext.SaveChangesAsync();
    }
}
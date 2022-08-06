using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Posts;
using Xunit;

namespace Tests; 

public class PostsServiceTests {
    private readonly ForumDbContext dbContext;
    private readonly PostsService postsService;
    
    public PostsServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        dbContext = new ForumDbContext(options);

        postsService = new PostsService(dbContext);
    }

    [Fact]
    public async Task Get_PostById_ReturnsOnePostWithCertainId() {
        await postsService.AddAsync("Title", "Desciption", "authorId", 1);

        int postId = (await dbContext.Posts.FirstAsync()).Id;
        Post post = await postsService.GetById(postId);

        Assert.Equal("Title", post.Title);
        Assert.Equal("Description", post.Description);
        Assert.Equal("authorId", post.AuthorId);
        Assert.Equal(1, post.SectionId);
    }

    [Fact]
    public async Task Get_AllPosts_ReturnsAllPostsWithinSection() {
        await postsService.AddAsync("Title1", "Desc1", "1", 1);
        await postsService.AddAsync("Title2", "Desc2", "2", 1);
        await postsService.AddAsync("Title3", "Desc3", "1", 1);
        await postsService.AddAsync("Title4", "Desc4", "1", 2);

        var posts = await postsService.GetBySection(1);

        Assert.Equal(3, posts.Count());
    }

    [Theory]
    [InlineData("SampleTitle", "SampleDescription")]
    [InlineData("LongTitleWithŚĆŚŻ", "")]
    public async Task Add_NewPost_IsVisibleInDatabase(string title, string description) {
        await postsService.AddAsync(title, description, "authorId", 1);

        Assert.Single(dbContext.Posts);
    }

    [Fact]
    public async Task Update_AddedPost_PostShouldHaveChangedData() {
        Post post = new() {
            Title = "Title",
            Description = "Description",
            AuthorId = "authorId",
            SectionId = 1,
            CreatedDate = DateTime.Now,
        };

        const string newTitle = "NewTitle";
        const string newDescription = "NewDescription";

        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        int postId = (await dbContext.Posts.FirstAsync()).Id;
        await postsService.UpdateAsync(postId, newTitle, newDescription);

        Post updatedPost = await dbContext.Posts.FindAsync(postId);

        Assert.Equal(newTitle, updatedPost.Title);
        Assert.Equal(newDescription, updatedPost.Description);
    }

    [Fact]
    public async Task Delete_AddedPost_PostShouldNotBeExisting() {
        Post post = new() {
            Title = "Title",
            Description = "Description",
            AuthorId = "authorId",
            SectionId = 1,
            CreatedDate = DateTime.Now,
        };

        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        Assert.Single(dbContext.Posts);

        int postId = (await dbContext.Posts.FirstAsync()).Id;
        await postsService.DeleteAsync(postId);

        Assert.Empty(dbContext.Posts);
    }
}
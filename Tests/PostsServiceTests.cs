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

        postsService = new PostsService(dbContext, null);
    }

    [Fact]
    public async Task Get_PostById_ReturnsOnePostWithCertainId() {
        await postsService.AddAsync("Title", "Description", "authorId", 1, null);

        int postId = (await dbContext.Posts.FirstAsync()).Id;
        Post post = await postsService.GetById(postId);

        Assert.Equal("Title", post.Title);
        Assert.Equal("Description", post.Description);
        Assert.Equal("authorId", post.AuthorId);
        Assert.Equal(1, post.SectionId);
    }

    [Fact]
    public async Task Get_AllPosts_ReturnsAllPostsWithinSection() {
        await postsService.AddAsync("Title1", "Desc1", "1", 1, null);
        await postsService.AddAsync("Title2", "Desc2", "2", 1, null);
        await postsService.AddAsync("Title3", "Desc3", "1", 1, null);
        await postsService.AddAsync("Title4", "Desc4", "1", 2, null);

        var posts = await postsService.GetBySection(1);

        Assert.Equal(3, posts.Count());
    }

    [Theory]
    [InlineData("SampleTitle", "SampleDescription")]
    [InlineData("LongTitleWithŚĆŚŻ", "")]
    public async Task Add_NewPost_IsVisibleInDatabase(string title, string description) {
        await postsService.AddAsync(title, description, "authorId", 1, null);

        Assert.Single(dbContext.Posts);
    }

    [Theory]
    [InlineData("1", "1", true)]
    [InlineData("1", "2", false)]
    [InlineData("111", "112", false)]
    public async Task Check_PostAndAuthor_ReturnInformationIfIsAuthorOfPost(string authorId, string otherAuthorId, bool expected) {
        await postsService.AddAsync("Title", "Desc", authorId, 1, null);

        bool isAuthor = await postsService.IsAuthor(1, otherAuthorId);

        Assert.Equal(isAuthor, expected);
    }

    [Fact]
    public async Task Get_CommentCount_GetCommentCountUnderPost() {
        Post post = new() {
            AuthorId = "1",
            SectionId = 1,
            CreatedDate = DateTime.Now,
            Description = "Desc",
            Title = "Title"
        };
        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        int postId = (await dbContext.Posts.FirstAsync()).Id;

        Comment[] comments = {
            new Comment { AuthorId = "2", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment { AuthorId = "3", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment { AuthorId = "4", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId },
            new Comment {
                AuthorId = "5", Description = "Desc", CreatedTime = DateTime.Now, PostId = postId - 1 //different post id
            }, 
        };

        await dbContext.AddRangeAsync(comments);
        await dbContext.SaveChangesAsync();

        int commentCount = await postsService.GetCommentCount(postId);

        Assert.Equal(3, commentCount);
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
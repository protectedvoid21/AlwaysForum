using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Services.Comments;
using Xunit;

namespace Tests; 

public class CommentsServiceTests {
    private readonly ForumDbContext dbContext;
    private readonly CommentsService commentsService;

    public CommentsServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        dbContext = new ForumDbContext(options);

        commentsService = new CommentsService(dbContext);
    }

    [Fact]
    public async Task Add_CommentToPost_ShouldBeVisibleInDatabase() {
        await commentsService.AddAsync("Description", 1, "authorId");

        Assert.Single(dbContext.Comments);
    }

    [Fact]
    public async Task Get_Comments_ReturnsAllCommentsFromOnePost() {
        await commentsService.AddAsync("Desc1", 1, "authorId");
        await commentsService.AddAsync("Desc2", 1, "authorId");
        await commentsService.AddAsync("Desc3", 2, "authorId");

        IEnumerable<Comment> comments = await commentsService.GetByPost(1);

        Assert.Equal(2, comments.Count());
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(7)]
    public async Task Get_CommentCount_ReturnsCommentCountForPost(int commentCount) {
        Random random = new();
        int randomCommentCount = random.Next(2, 5);

        Comment[] comments = new Comment[commentCount + randomCommentCount];
        for (int i = 0; i < commentCount; i++) {
            comments[i] = new() {
                Description = $"Desc{i}",
                AuthorId = $"authorId{i}",
                PostId = 1,
            };
        }

        //other post id to make sure it doesn't count every comment
        for (int i = 0; i < randomCommentCount; i++) {
            comments[i + commentCount] = new() {
                Description = $"Desc{i}",
                AuthorId = $"authorId{i}",
                PostId = 2,
            };
        }

        await dbContext.AddRangeAsync(comments);
        await dbContext.SaveChangesAsync();

        int commentCountFromService = await commentsService.GetCountInPost(1);
        Assert.Equal(commentCount, commentCountFromService);
    }

    [Fact]
    public async Task Update_CertainComment_ChangesShouldBeVisible() {
        await commentsService.AddAsync("Desc1", 1, "authorId");

        Assert.Single(dbContext.Comments);

        int commentId = (await dbContext.Comments.FirstAsync()).Id;
        await commentsService.UpdateAsync(commentId, "UpdatedDesc");

        Comment updatedComment = await dbContext.Comments.FirstAsync();
        Assert.Equal("UpdatedDesc", updatedComment.Description);
    }

    [Fact]
    public async Task Delete_Comment_DatabaseShouldHaveNoticeDeletion() {
        await commentsService.AddAsync("Desc", 1, "authorId");

        Assert.Single(dbContext.Comments);

        int commentId = (await dbContext.Comments.FirstAsync()).Id;
        await commentsService.DeleteAsync(commentId);

        Assert.Empty(dbContext.Comments);
    }
}
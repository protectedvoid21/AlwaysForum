using Data;
using Data.Models;
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
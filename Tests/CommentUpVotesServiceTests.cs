using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.CommentUpVotes;
using Xunit;

namespace Tests;

public class CommentUpVotesServiceTests {
    private readonly ForumDbContext dbContext;
    private readonly CommentUpVotesService commentUpVotesService;

    public CommentUpVotesServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        dbContext = new ForumDbContext(options);

        commentUpVotesService = new CommentUpVotesService(dbContext);
    }

    [Fact]
    public async Task Add_NewVote_AddsVoteToDatabaseAndItIsSeenInDb() {
        await commentUpVotesService.VoteAsync(1, "1", true);

        Assert.Single(dbContext.CommentUpVotes);
    }

    [Fact]
    public async Task TryAdd_ExistingVote_VoteIsDeletedFromDatabase() {
        await commentUpVotesService.VoteAsync(1, "1", true);

        await commentUpVotesService.VoteAsync(1, "1", true);

        Assert.Empty(dbContext.CommentUpVotes);
    }

    [Fact]
    public async Task TryAdd_ExistingVoteWithOtherType_VoteHasChangedItsIsUpVoteProperty() {
        await commentUpVotesService.VoteAsync(1, "1", true);

        await commentUpVotesService.VoteAsync(1, "1", false);

        CommentUpVote vote = await dbContext.CommentUpVotes.FirstAsync();

        Assert.Single(dbContext.CommentUpVotes);
        Assert.False(vote.IsUpVote);
    }

    [Fact]
    public async Task Get_VoteCount_ReturnsUpVotesMinusDownVotes() {
        CommentUpVote[] votes = {
            new() { CommentId = 1, AuthorId = "1", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "2", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "3", IsUpVote = false },
            new() { CommentId = 1, AuthorId = "4", IsUpVote = false },
            new() { CommentId = 1, AuthorId = "5", IsUpVote = true },
            new() { CommentId = 1, AuthorId = "6", IsUpVote = true },
            new() { CommentId = 2, AuthorId = "1", IsUpVote = true },
            new() { CommentId = 2, AuthorId = "2", IsUpVote = true },
        };

        await dbContext.CommentUpVotes.AddRangeAsync(votes);
        await dbContext.SaveChangesAsync();

        int voteCount = await commentUpVotesService.GetVoteCount(1);

        Assert.Equal(2, voteCount);
    }
}
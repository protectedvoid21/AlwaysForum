using System.Runtime.CompilerServices;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Reactions;
using Xunit;

namespace Tests; 

public class ReactionsServiceTests {
    private readonly ForumDbContext dbContext;
    private readonly ReactionsService reactionsService;
    
    public ReactionsServiceTests() {
        var options = new DbContextOptionsBuilder<ForumDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        dbContext = new ForumDbContext(options);

        reactionsService = new ReactionsService(dbContext);
    }

    [Fact]
    public async Task Add_NewReaction_ReactionIsVisibleInDatabase() {
        await reactionsService.ReactAsync(1, "1", ReactionType.Like);

        Assert.Single(dbContext.Reactions);
    }

    [Fact]
    public async Task Get_ByPost_ReturnsAllReactionInPost() {
        IEnumerable<Post> posts = new[] {
            new Post { Title = "Title1", Description = "Desc1", AuthorId = "1", CreatedDate = DateTime.Now, SectionId = 1 },
            new Post { Title = "Title2", Description = "Desc2", AuthorId = "1", CreatedDate = DateTime.Now, SectionId = 1 },
        };
        await dbContext.AddRangeAsync(posts);

        IEnumerable<Reaction> reactions = new[] {
            new Reaction { PostId = 1, UserId = "1", ReactionType = ReactionType.Like },
            new Reaction { PostId = 1, UserId = "2", ReactionType = ReactionType.Sad },
            new Reaction { PostId = 1, UserId = "3", ReactionType = ReactionType.Love },
            new Reaction { PostId = 1, UserId = "4", ReactionType = ReactionType.Angry },
            new Reaction { PostId = 2, UserId = "1", ReactionType = ReactionType.Wow },
            new Reaction { PostId = 2, UserId = "2", ReactionType = ReactionType.Angry },
            new Reaction { PostId = 2, UserId = "3", ReactionType = ReactionType.Like },
        };

        await dbContext.AddRangeAsync(reactions);
        await dbContext.SaveChangesAsync();

        var firstPostReactions = await reactionsService.GetByPost(1);
        var secondPostReactions = await reactionsService.GetByPost(2);

        Assert.Equal(4, firstPostReactions.Count());
        Assert.Equal(3, secondPostReactions.Count());
    }

    [Fact]
    public async Task Update_ExistingReaction_ShouldChangeItsType() {
        await dbContext.Reactions.AddAsync(new Reaction {
            PostId = 1,
            UserId = "1",
            ReactionType = ReactionType.Like
        });
        await dbContext.SaveChangesAsync();

        Reaction reaction = await dbContext.Reactions.FirstAsync();

        await reactionsService.UpdateAsync(reaction.Id, ReactionType.Love);

        Assert.Equal(ReactionType.Love, reaction.ReactionType);
    }

    [Fact]
    public async Task Remove_Reaction_IsNotExistingInDatabase() {
        await dbContext.Reactions.AddAsync(new Reaction {
            PostId = 1,
            UserId = "1",
            ReactionType = ReactionType.Like
        });
        await dbContext.SaveChangesAsync();

        Assert.Single(dbContext.Reactions);

        Reaction reaction = await dbContext.Reactions.FirstAsync();
        await reactionsService.RemoveAsync(reaction.Id);

        Assert.Empty(dbContext.Reactions);
    }
}
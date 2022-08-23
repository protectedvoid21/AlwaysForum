using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Reactions; 

public class ReactionsService : IReactionsService {
    private readonly ForumDbContext dbContext;

    public ReactionsService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task ReactAsync(int postId, string userId, ReactionType reactionType) {
        Reaction? existingReaction = await dbContext.Reactions.FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);
        if (existingReaction != null) {
            if (existingReaction.ReactionType == reactionType) {
                await RemoveAsync(existingReaction.Id);
                return;
            }

            await UpdateAsync(existingReaction.Id, reactionType);
            return;
        }

        Reaction reaction = new() {
            PostId = postId,
            UserId = userId,
            ReactionType = reactionType
        };

        await dbContext.AddAsync(reaction);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpVoteComment(int commentId, string userId, bool isUpVote) {
        
    }

    public async Task<IEnumerable<Reaction>> GetByPost(int postId) {
        return dbContext.Reactions.Where(r => r.PostId == postId);
    }

    public async Task UpdateAsync(int id, ReactionType reactionType) {
        Reaction reaction = await dbContext.Reactions.FindAsync(id);
        if (reaction == null) {
            return;
        }

        reaction.ReactionType = reactionType;

        dbContext.Reactions.Update(reaction);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id) {
        Reaction reaction = await dbContext.Reactions.FindAsync(id);
        if(reaction == null) {
            return;
        }

        dbContext.Reactions.Remove(reaction);
        await dbContext.SaveChangesAsync();
    }
}
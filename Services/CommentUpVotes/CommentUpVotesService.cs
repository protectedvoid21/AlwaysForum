using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.CommentUpVotes; 

public class CommentUpVotesService : ICommentUpVotesService {
    private readonly ForumDbContext dbContext;

    public CommentUpVotesService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task VoteAsync(int commentId, string userId, bool isUpVote) {
        CommentUpVote? vote = await dbContext.CommentUpVotes.FirstOrDefaultAsync(c => c.CommentId == commentId && c.AuthorId == userId);
        if (vote != null) {
            if (vote.IsUpVote == isUpVote) {
                dbContext.Remove(vote);
                await dbContext.SaveChangesAsync();
                return;
            }

            vote.IsUpVote = isUpVote;
            dbContext.Update(vote);
            await dbContext.SaveChangesAsync();
            return;
        }

        vote = new CommentUpVote {
            CommentId = commentId,
            AuthorId = userId,
            IsUpVote = isUpVote,
        };

        await dbContext.AddAsync(vote);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsVotedByUser(int commentId, string userId) {
        return await dbContext.CommentUpVotes.AnyAsync(c => c.AuthorId == userId);
    }

    public async Task<int> GetVoteCount(int commentId) {
        IEnumerable<CommentUpVote> votes = dbContext.CommentUpVotes.Where(c => c.CommentId == commentId);

        return votes.Count(v => v.IsUpVote) - votes.Count(v => v.IsUpVote == false);
    }
}
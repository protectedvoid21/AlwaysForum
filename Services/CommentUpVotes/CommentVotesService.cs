using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.CommentUpVotes; 

public class CommentVotesService : ICommentVotesService {
    private readonly ForumDbContext dbContext;

    public CommentVotesService(ForumDbContext dbContext) {
        this.dbContext = dbContext;
    }

    public async Task VoteAsync(int commentId, string userId, bool isUpVote) {
        CommentVote? vote = await dbContext.CommentUpVotes.FirstOrDefaultAsync(c => c.CommentId == commentId && c.AuthorId == userId);
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

        vote = new CommentVote {
            CommentId = commentId,
            AuthorId = userId,
            IsUpVote = isUpVote,
        };

        await dbContext.AddAsync(vote);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CommentVoteStatus> IsVotedByUser(int commentId, string userId) {
        CommentVote? commentVote = await dbContext.CommentUpVotes.FirstOrDefaultAsync(c => c.AuthorId == userId && c.CommentId == commentId);
        if (commentVote == null) {
            return CommentVoteStatus.None;
        }

        return commentVote.IsUpVote ? CommentVoteStatus.UpVoted : CommentVoteStatus.DownVoted;
    }

    public async Task<int> GetVoteCount(int commentId) {
        IEnumerable<CommentVote> votes = dbContext.CommentUpVotes.Where(c => c.CommentId == commentId);

        return votes.Count(v => v.IsUpVote) - votes.Count(v => v.IsUpVote == false);
    }
}
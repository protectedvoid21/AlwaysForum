namespace Services.CommentUpVotes;

public interface ICommentUpVotesService {
    Task VoteAsync(int commentId, string userId, bool isUpVote);

    Task<bool> IsVotedByUser(int commentId, string userId);

    Task<int> GetVoteCount(int commentId);
}
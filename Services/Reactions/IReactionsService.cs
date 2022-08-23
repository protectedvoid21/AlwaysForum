using Data.Models;

namespace Services.Reactions; 

public interface IReactionsService {
    Task ReactAsync(int postId, string userId, ReactionType reactionType);

    Task UpVoteComment(int commentId, string userId, bool isUpVote);

    Task<IEnumerable<Reaction>> GetByPost(int postId);

    Task UpdateAsync(int id, ReactionType reactionType);

    Task RemoveAsync(int id);
}
using Data.Models;

namespace Services.Comments; 

public interface ICommentsService {
    Task AddAsync(string description, int postId, string authorId);

    Task<IEnumerable<Comment>> GetByPost(int postId);

    Task<bool> IsAuthor(int commentId, string authorId);

    Task<int> GetCountInPost(int postId);

    Task UpdateAsync(int id, string description);

    Task DeleteAsync(int id);
}
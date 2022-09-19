using Data.Models;

namespace Services.Posts; 

public interface IPostsService {
    Task<Post> GetById(int id);

    Task<IEnumerable<Post>> GetBySection(int sectionId);

    Task<int> GetCommentCount(int id);

    Task<int> AddAsync(string title, string description, string authorId, int sectionId, IEnumerable<int> tagIds);

    Task<bool> IsAuthor(int postId, string authorId);

    Task UpdateAsync(int id, string title, string description, IEnumerable<int> tagIds);

    Task DeleteAsync(int id);
}
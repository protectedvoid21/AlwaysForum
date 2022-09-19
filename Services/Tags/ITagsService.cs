using Data.Models;
using Data.ViewModels;

namespace Services.Tags; 

public interface ITagsService {
    Task AddAsync(string name);

    Task AddToPost(int tagId, int postId);

    Task UpdateAsync(int id, string name);

    Task<IEnumerable<Tag>> GetAllAsync();

    Task<IEnumerable<TagViewModel>> GetTrendingForSection(int sectionId, int count);

    Task DeleteAsync(int id);
}
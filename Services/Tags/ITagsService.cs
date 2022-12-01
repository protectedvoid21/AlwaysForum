using Data.Models;
using Data.ViewModels.Tag;

namespace Services.Tags;

public interface ITagsService {
    Task AddAsync(string name);

    Task AddToPost(int tagId, int postId);

    Task UpdateAsync(int id, string name);

    Task UpdateTagsOnPost(int postId, IEnumerable<int> tagIds);

    Task<Tag> GetById(int id);

    Task<IEnumerable<Tag>> GetAllAsync();

    Task<IEnumerable<TagViewModel>> GetTrendingForSection(int sectionId, int count);

    Task DeleteAsync(int id);
}
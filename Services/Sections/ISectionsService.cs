using Data.Models;

namespace Services.Sections; 

public interface ISectionsService {
    Task AddAsync(string name, string description);

    Task<Section> GetById(int id);

    Task<IEnumerable<Section>> GetAll();

    Task<int> GetPostCount(int id);

    Task UpdateAsync(int id, string name, string description);

    Task DeleteAsync(int id);
}
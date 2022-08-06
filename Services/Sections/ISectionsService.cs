using Data.Models;

namespace Services.Sections; 

public interface ISectionsService {
    Task AddAsync(string name, string description);

    Task<Section> GetById(int id);

    Task<IEnumerable<Section>> GetAll();

    Task UpdateAsync(int id, string name, string description);

    Task DeleteAsync(int id);
}
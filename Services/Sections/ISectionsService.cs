using Data.Models;

namespace Services.Sections; 

public interface ISectionsService {
    Task AddAsync(string name);

    Task<IEnumerable<Section>> GetAll();

    Task UpdateAsync(int id, string name);

    Task DeleteAsync(int id);
}
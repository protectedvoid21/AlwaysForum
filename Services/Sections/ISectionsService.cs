using Data.Models;

namespace Services.Sections; 

public interface ISectionsService {
    Task AddAsync(string name, string description);

    Task<TSection> GetById<TSection>(int id);

    Task<IEnumerable<TModel>> GetAll<TModel>();

    Task<int> GetPostCount(int id);

    Task UpdateAsync(int id, string name, string description);

    Task DeleteAsync(int id);
}
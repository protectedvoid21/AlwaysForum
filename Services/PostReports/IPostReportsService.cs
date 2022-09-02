using Data.Models;

namespace Services.PostReports; 

public interface IPostReportsService {
    Task AddAsync(int postId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<TModel>> GetAll<TModel>();

    Task DeleteAsync(int id);
}
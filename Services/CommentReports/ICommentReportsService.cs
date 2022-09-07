using Data.Models;

namespace Services.CommentReports; 

public interface ICommentReportsService {
    Task AddAsync(int commentId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<TModel>> GetAll<TModel>();

    Task DeleteAsync(int id);
}
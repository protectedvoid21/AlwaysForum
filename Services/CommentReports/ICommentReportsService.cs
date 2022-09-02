using Data.Models;

namespace Services.CommentReports; 

public interface ICommentReportsService {
    Task AddAsync(int commentId, string authorId, int reportTypeId, string description);

    Task<IEnumerable<CommentReport>> GetAll();

    Task DeleteAsync(int id);
}
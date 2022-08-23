using Data.Models;

namespace Data.ViewModels; 

public class CommentViewModel {
    public int Id { get; set; }
    public string Description { get; set; }
    public ForumUser Author { get; set; }
    public int LikeCount { get; set; }
}
using Data.Models;

namespace Data.ViewModels.Post; 

public class PostViewModel {
    public string Title { get; set; }
    public string Description { get; set; }
    public ForumUser Author { get; set; }
    public List<Comment> Comments { get; set; }
}
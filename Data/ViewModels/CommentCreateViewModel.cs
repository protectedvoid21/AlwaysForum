using Data.Models;

namespace Data.ViewModels; 

public class CommentCreateViewModel {
    public string Description { get; set; }

    public int PostId { get; set; }
}
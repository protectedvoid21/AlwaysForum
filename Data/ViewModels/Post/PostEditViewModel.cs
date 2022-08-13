using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Post;

public class PostEditViewModel {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
}
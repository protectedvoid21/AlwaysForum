using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.ViewModels.Post;

public class PostCreateViewModel {
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int SectionId { get; set; }
}
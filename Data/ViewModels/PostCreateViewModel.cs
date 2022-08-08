using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels; 

public class PostCreateViewModel {
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int SectionId { get; set; }
}
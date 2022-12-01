using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Post;

public class PostCreateViewModel {
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int SectionId { get; set; }

    [MaxLength(3)]
    public IEnumerable<int> SelectedTags { get; set; }

    public IEnumerable<Models.Tag> TagList { get; set; }
}
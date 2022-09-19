using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.ViewModels.Post;

public class PostEditViewModel {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [MaxLength(GlobalConstants.MaxTagsOnPost)]
    public IEnumerable<int> SelectedTags { get; set; }

    public IEnumerable<Tag> TagList { get; set; }
}
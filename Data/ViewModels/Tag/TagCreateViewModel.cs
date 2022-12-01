using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Tag;

public class TagCreateViewModel {
    [Required]
    public string Name { get; set; }
}
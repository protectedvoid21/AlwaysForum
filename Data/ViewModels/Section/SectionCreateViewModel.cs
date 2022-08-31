using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Section;

public class SectionCreateViewModel {
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}
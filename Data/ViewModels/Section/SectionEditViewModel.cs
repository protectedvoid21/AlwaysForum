using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Section; 

public class SectionEditViewModel {
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}
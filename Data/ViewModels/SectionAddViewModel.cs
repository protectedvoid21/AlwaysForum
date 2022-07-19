using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels; 

public class SectionAddViewModel {
    [Required]
    public string Name { get; set; }
}
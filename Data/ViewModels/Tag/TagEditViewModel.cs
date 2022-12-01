using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels.Tag; 

public class TagEditViewModel {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
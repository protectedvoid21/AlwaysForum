using Data.Models;

namespace Data.ViewModels; 

public class SectionViewModel {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<SectionPostViewModel> PostsModels { get; set; }
}
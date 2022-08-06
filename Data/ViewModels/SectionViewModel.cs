using Data.Models;

namespace Data.ViewModels; 

public class SectionViewModel {
    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<Post> Posts { get; set; }
}
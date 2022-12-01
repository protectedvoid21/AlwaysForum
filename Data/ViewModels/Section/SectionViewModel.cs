using Data.Models;
using Data.ViewModels.Tag;

namespace Data.ViewModels.Section;

public class SectionViewModel {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<SectionPostViewModel> PostsModels { get; set; }

    public IEnumerable<TagViewModel> PopularTags { get; set; }
}
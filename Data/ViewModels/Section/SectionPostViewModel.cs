using Data.Models;

namespace Data.ViewModels.Section;

public class SectionPostViewModel {
    public int Id { get; set; }

    public string Title { get; set; }

    public string ShortenedDescription { get; set; }

    public string AuthorName { get; set; }

    public string AuthorId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CommentCount { get; set; }

    public IEnumerable<string> TagsList { get; set; }
}
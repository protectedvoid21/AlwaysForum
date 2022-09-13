namespace Data.Models; 

public class PostReport {
    public int Id { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string AuthorId { get; set; }

    public ForumUser Author { get; set; }

    public int ReportTypeId { get; set; }

    public ReportType ReportType { get; set; }
}
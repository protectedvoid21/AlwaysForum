namespace Data.Models; 

public class CommentReport {
    public int Id { get; set; }

    public int? CommentId { get; set; }

    public Comment Comment { get; set; }

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public string AuthorId { get; set; }

    public ForumUser Author { get; set; }

    public int ReportTypeId { get; set; }

    public ReportType ReportType { get; set; }
}
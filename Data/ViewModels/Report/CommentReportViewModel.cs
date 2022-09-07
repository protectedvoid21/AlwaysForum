namespace Data.ViewModels.Report; 

public class CommentReportViewModel {
    public int Id { get; set; }

    public string AuthorUserName { get; set; }

    public string CommentText { get; set; }

    public int CommentId { get; set; }

    public string PostTitle { get; set; }

    public int PostId { get; set; }

    public string? AuthorId { get; set; }

    public string Description { get; set; }

    public string ReportTypeName { get; set; }

    public DateTime CreateDate { get; set; }
}
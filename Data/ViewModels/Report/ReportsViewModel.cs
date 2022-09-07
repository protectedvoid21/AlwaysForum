using Data.Models;

namespace Data.ViewModels.Report; 

public class ReportsViewModel {
    public IEnumerable<PostReportViewModel> PostReports { get; set; }

    public IEnumerable<CommentReportViewModel> CommentReports { get; set; }
}
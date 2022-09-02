using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.ViewModels.Report;

public class ReportCreateViewModel {
    [MaxLength(120)]
    public string? Description { get; set; }

    [Required]
    public int ObjectId { get; set; }

    public ReportTarget ReportTarget { get; set; }

    public int ReportTypeId { get; set; }

    public List<ReportType> ReportTypes { get; set; } = new();
}
using Data.ViewModels.Report;
using Microsoft.AspNetCore.Mvc;
using Services.ReportTypes;

namespace AlwaysForum.Components;

public class CreateReportViewComponent : ViewComponent {
    private readonly IReportTypesService reportTypesService;

    public CreateReportViewComponent(IReportTypesService reportTypesService) {
        this.reportTypesService = reportTypesService;
    }

    public async Task<IViewComponentResult> InvokeAsync(ReportComponentData reportData) {
        ReportCreateViewModel reportModel = new() {
            ObjectId = reportData.ObjectId,
            ReportTypes = (await reportTypesService.GetAllAsync()).ToList(),
            ReportTarget = reportData.ReportTarget
        };
        return View(reportModel);
    }
}
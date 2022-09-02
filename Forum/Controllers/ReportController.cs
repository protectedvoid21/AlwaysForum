using AlwaysForum.Extensions;
using AutoMapper.Configuration.Annotations;
using Data;
using Data.ViewModels;
using Data.ViewModels.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.CommentReports;
using Services.PostReports;

namespace AlwaysForum.Controllers;

[Authorize]
public class ReportController : Controller {
    private readonly IPostReportsService postReportsService;
    private readonly ICommentReportsService commentReportsService;

    public ReportController(IPostReportsService postReportsService, ICommentReportsService commentReportsService) {
        this.postReportsService = postReportsService;
        this.commentReportsService = commentReportsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReportCreateViewModel reportModel) {
        if (!ModelState.IsValid) {
            return InfoHelper.RedirectToMessage("Could not send the report", InfoType.Error);
        }

        switch (reportModel.ReportTarget) {
            case ReportTarget.Post:
                await postReportsService.AddAsync(reportModel.ObjectId, User.GetId(), reportModel.ReportTypeId, reportModel.Description);
                break;
            case ReportTarget.Comment:
                await commentReportsService.AddAsync(reportModel.ObjectId, User.GetId(), reportModel.ReportTypeId, reportModel.Description);
                break;
        }
        return InfoHelper.RedirectToMessage("Report has been sent to administrators", InfoType.Success);
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> ViewAll() {
        ReportsViewModel reportsModel = new() {
            PostReports = await postReportsService.GetAll<PostReportViewModel>(),
            CommentReports = await commentReportsService.GetAll(),
        };

        return View(reportsModel);
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> RemovePostReport(int id) {
        await postReportsService.DeleteAsync(id);
        return RedirectToAction("ViewAll");
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> RemoveCommentReport(int id) {
        await commentReportsService.DeleteAsync(id);
        return RedirectToAction("ViewAll");
    }
}
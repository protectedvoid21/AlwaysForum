using Data;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Section;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Services.Sections;
using Services.Tags;

namespace AlwaysForum.Controllers;

public class SectionController : Controller {
    private readonly ISectionsService sectionsService;
    private readonly ITagsService tagsService;

    public SectionController(ISectionsService sectionsService, ITagsService tagsService) {
        this.sectionsService = sectionsService;
        this.tagsService = tagsService;
    }

    public async Task<IActionResult> View(int sectionId) {
        SectionViewModel sectionModel = await sectionsService.GetById<SectionViewModel>(sectionId);
        sectionModel.PopularTags = await tagsService.GetTrendingForSection(sectionId, 5);
        return View(sectionModel);
    }

    [HttpGet, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public IActionResult Add() => View();

    [HttpPost, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Add(SectionCreateViewModel sectionModel) {
        if(!ModelState.IsValid) {
            return View(sectionModel);
        }

        await sectionsService.AddAsync(sectionModel.Name, sectionModel.Description);

        return RedirectToAction("Panel", "Admin");
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> SectionList() {
        IEnumerable<Section> sectionList = await sectionsService.GetAll();
        return View(sectionList);
    }

    [HttpGet, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Edit(int id) {
        SectionEditViewModel sectionModel = await sectionsService.GetById<SectionEditViewModel>(id);
        return View(sectionModel);
    }

    [HttpPost, Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Edit(SectionEditViewModel sectionModel) {
        if (!ModelState.IsValid) {
            return View(sectionModel);
        }

        await sectionsService.UpdateAsync(sectionModel.Id, sectionModel.Name, sectionModel.Description);
        return RedirectToAction("View", new { sectionId = sectionModel.Id });
    }

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public async Task<IActionResult> Delete(int id) {
        await sectionsService.DeleteAsync(id);
        return InfoHelper.RedirectToMessage("Section has been deleted", InfoType.Success);
    }
}
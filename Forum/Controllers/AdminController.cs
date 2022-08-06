using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Sections;

namespace AlwaysForum.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller {
    private readonly ISectionsService sectionsService;

    public AdminController(ISectionsService sectionsService) {
        this.sectionsService = sectionsService;
    }

    public ViewResult Panel() => View();

    [HttpGet]
    public IActionResult AddSection() => View();

    [HttpPost]
    public async Task<IActionResult> AddSection(SectionAddViewModel sectionModel) {
        if (!ModelState.IsValid) {
            return View(sectionModel);
        }

        await sectionsService.AddAsync(sectionModel.Name, sectionModel.Description);

        return RedirectToAction("Panel");
    }
}
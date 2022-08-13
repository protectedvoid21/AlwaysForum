using System.Diagnostics;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Sections;

namespace AlwaysForum.Controllers; 

public class HomeController : Controller {
    private readonly ISectionsService sectionsService;

    public HomeController(ISectionsService sectionsService) {
        this.sectionsService = sectionsService;
    }

    public async Task<IActionResult> Index() {
        var sectionList = await sectionsService.GetAll();
        return View(sectionList);
    }

    public ViewResult Message(MessageViewModel messageModel) {
        return View(messageModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
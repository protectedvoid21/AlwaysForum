using System.Diagnostics;
using Data.Models;
using Data.ViewModels;
using Data.ViewModels.Section;
using Microsoft.AspNetCore.Mvc;
using Services.Messages;
using Services.Sections;

namespace AlwaysForum.Controllers; 

public class HomeController : Controller {
    private readonly ISectionsService sectionsService;
    private readonly IMessagesService messagesService;

    public HomeController(ISectionsService sectionsService, IMessagesService messagesService) {
        this.sectionsService = sectionsService;
        this.messagesService = messagesService;
    }

    public async Task<IActionResult> Index() {
        var sectionList = await sectionsService.GetAll<SectionStartViewModel>();
        return View(sectionList);
    }

    public ViewResult GetInfo(InfoViewModel infoModel) {
        return View(infoModel);
    }

    public async Task<IActionResult> Chat() {
        var messages = await messagesService.GetLastMessages(10);
        return View(messages);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using Data;
using Data.Models;
using Data.ViewModels.Section;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Sections;

namespace AlwaysForum.Controllers;

[Authorize(Roles = GlobalConstants.AdminRoleName)]
public class AdminController : Controller {
    private readonly ISectionsService sectionsService;

    public AdminController(ISectionsService sectionsService) {
        this.sectionsService = sectionsService;
    }

    public ViewResult Panel() => View();
}
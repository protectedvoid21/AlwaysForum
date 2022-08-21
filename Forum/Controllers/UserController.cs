using AlwaysForum.Extensions;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace AlwaysForum.Controllers; 

public class UserController : Controller {
    private readonly IUsersService usersService;

    public UserController(IUsersService usersService) {
        this.usersService = usersService;
    }

    public async Task<IActionResult> Profile(string userId) {
        var profileModel = await usersService.GetProfile(userId);
        return View(profileModel);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeProfilePicture(ChangeProfilePictureViewModel profilePictureModel) {
        await usersService.ChangeProfilePicture(User.GetId(), profilePictureModel.NewProfilePicture);
        return RedirectToAction("Profile", new { userId = profilePictureModel.UserId });
    }
}
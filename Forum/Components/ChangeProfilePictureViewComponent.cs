using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Components; 

public class ChangeProfilePictureViewComponent : ViewComponent {
    public async Task<IViewComponentResult> InvokeAsync(string currentProfilePicture) {
        ChangeProfilePictureViewModel profileModel = new() {
            ProfilePicture = currentProfilePicture
        };
        return View(profileModel);
    }
}
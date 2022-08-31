using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Data; 

public static class InfoHelper {
    public static IActionResult RedirectToMessage(string description, InfoType infoType) {
        InfoViewModel infoModel = new() { Description = description, InfoType = infoType };
        return new RedirectToActionResult("GetInfo", "Home", infoModel);
    }
}
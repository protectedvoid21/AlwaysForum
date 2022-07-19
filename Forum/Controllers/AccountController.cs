using Data.Models;
using Data.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Controllers; 

public class AccountController : Controller {
    private readonly UserManager<ForumUser> userManager;
    private readonly SignInManager<ForumUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(UserManager<ForumUser> userManager, SignInManager<ForumUser> signInManager, RoleManager<IdentityRole> roleManager) {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    public ViewResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginModel) {
        if (!ModelState.IsValid) {
            return View(loginModel);
        }

        var user = await userManager.FindByEmailAsync(loginModel.Email);
        await signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

        return RedirectToAction("Index", "Home");
    }
}
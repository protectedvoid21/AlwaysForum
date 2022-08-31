using Data;
using Data.Models;
using Data.ViewModels;
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
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    public ViewResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginModel) {
        if (!ModelState.IsValid) {
            return View();
        }

        var user = await userManager.FindByNameAsync(loginModel.UserName);
        var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

        if (!result.Succeeded) {
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ViewResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerModel) {
        if (!ModelState.IsValid) {
            return View();
        }

        ForumUser user = new() {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            CreatedDate = DateTime.Now,
        };

        var result = await userManager.CreateAsync(user, registerModel.Password);

        if (!result.Succeeded) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        return InfoHelper.RedirectToMessage("Now you can log in to your account to start with posting and commenting on AlwaysForum!", InfoType.Success);
    }

    public async Task<IActionResult> LogOut() {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
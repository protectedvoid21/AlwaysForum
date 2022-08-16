using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Controllers; 

public class UserController : Controller {
    /*private readonly UsersService usersService;

    public UserController(IUsersService usersService) {
        this.usersService = usersService;
    }*/

    public async Task<IActionResult> Profile(string userId) {
        throw new NotImplementedException();
    }
}
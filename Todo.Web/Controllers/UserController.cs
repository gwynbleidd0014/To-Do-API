using Microsoft.AspNetCore.Mvc;
using Todo.application.Users;
using Todo.application.Users.Request;

namespace Todo.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequestModel user, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return View();

        await _userService.Register(token, user);

        return RedirectToAction("CreateUser");
    }
}

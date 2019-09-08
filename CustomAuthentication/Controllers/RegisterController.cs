using CustomAuthentication.Models;
using CustomAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthentication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserService _userService;

        public RegisterController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            var checkedUser = _userService.CreateUser(user);
            if (checkedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login","Account");
        }


    }
}
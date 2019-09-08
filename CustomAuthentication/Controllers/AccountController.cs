using System;
using System.Security.Claims;
using CustomAuthentication.Models;
using CustomAuthentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            if (!string.IsNullOrEmpty(user.UserName) && string.IsNullOrEmpty(user.Password))
            {
                return RedirectToAction("Login");
            }

            if (isAdmin(user) || isAuthenticatedUser(user))
            {
                return RedirectToAction("Index", "Home");
            }
            //Burada DB kontrolü yap. Gelen kullanıcı adı ve şifreyi kontrol et. 
            
            ViewBag.ErrorMessage = "Hatalı kullanıcı adı veya parola girdiniz.";
            return View();
        }

        private bool isAuthenticatedUser(UserModel user)
        {
            UserModel newUser = _userService.ValidateUsernameAndEmail(user);
            if (newUser == null)
            {
                return false;
            }
            else
            {
                if (IsPasswordCorrect(user, newUser))
                {
                    authenticateUser(newUser);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsPasswordCorrect(UserModel user, UserModel newUser)
        {
            if (user.Password == newUser.Password)
            {
                return true;
            }
            return false;
        }

        public bool isAdmin(UserModel user)
        {
            if (user.UserName == "Admin" && user.Password == "Admin")
            {
                authenticateUser(user);
                return true;
            }

            return false;
        }
        
        public void authenticateUser(UserModel user)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


    }
}
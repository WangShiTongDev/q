using DevTeamUp.BLL.Services;
using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Filters;
using DevTeamUp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevTeamUp.Controllers
{
    //[ServiceFilter(typeof(ProfileCompletionFilter))]
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly UserService userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }


        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            var signInResult = signInManager.PasswordSignInAsync(model.Email, model.Password, true, false).Result;
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Msg = "Невірний логін або пароль";


            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            var newUser = new User
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = userManager.CreateAsync(newUser, model.Password).Result;
            _ = result;
            
            if (result.Succeeded)
            {
                signInManager.SignInAsync(newUser, true).Wait();
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("ProfileInit", "Profile");
            }

            ViewBag.Msg = "Схоже такий користувач вже існує";
            return View();
        }

        public IActionResult Logout()
        {

            signInManager.SignOutAsync();
            
            return RedirectToAction("Login", "Account");
        }
    }
}

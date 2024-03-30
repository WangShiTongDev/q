using DevTeamUp.DAL.EF.Entities;
using DevTeamUp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevTeamUp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

            ModelState.AddModelError(String.Empty, "BAN");


            return RedirectToAction("Index", "Home");
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


            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}

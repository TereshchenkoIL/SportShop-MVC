using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySportShop.Models.Models;
using MySportShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET
        [HttpGet]
        public IActionResult LogIn( string returnUrl = null)
        {
            return View(new LogInVM() { ReturnUrl = returnUrl });
        }

        // GET
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM { ReturnUrl ="/Home/Index"});
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { Email = registerVM.Email, UserName = registerVM.Email };

                if(string.Compare(registerVM.Password, registerVM.ConfirmPassword) != 0)
                {
                    ModelState.AddModelError("", "Passwords aren't coincided");
                    return View(registerVM);
                }

                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    RedirectToAction("Index", "Home");
                }
            }
            return View(registerVM);
        }

        // POST
        [HttpPost, ActionName("LogIn")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogIn(LogInVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                    .PasswordSignInAsync(loginVM.Email, loginVM.Password,
                    loginVM.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginVM.ReturnUrl) && Url.IsLocalUrl(loginVM.ReturnUrl))
                        return Redirect(loginVM.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Something wrong with login or password");
            }

            return View(loginVM);

        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

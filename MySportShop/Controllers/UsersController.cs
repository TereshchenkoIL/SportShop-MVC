using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySportShop.Models.Models;
using MySportShop.Models.ViewModel;
using MySportShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySportShop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IRepositoryManager _repositoryManager;
        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IRepositoryManager repositoryManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repositoryManager = repositoryManager;
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AppUser> model = await _repositoryManager.User.GetAllAsync(false);
            return View(model);
        }
        // GET
        [HttpGet]
        public IActionResult CreateUser()
        {

            return View(new RegisterVM());
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if(string.Compare(model.Password, model.ConfirmPassword) != 0)
                {
                    ModelState.AddModelError(String.Empty, "Passwords are not coincided");
                    return View(model);
                }

                var user = new AppUser { Email = model.Email, UserName = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(String.Empty, "Something went wrond during creating");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET
        [HttpGet]
        public IActionResult Delete(AppUser user)
        {
            return View(user);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index", "Home");
        }


        // GET
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var model = new ChangePasswordVM { Email = user.Email, Id = user.Id };
            return View(model);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                    return NotFound();
                var passwordValidator =
                       HttpContext.RequestServices.GetService(typeof(IPasswordValidator<AppUser>)) as IPasswordValidator<AppUser>;
                var passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<AppUser>)) as IPasswordHasher<AppUser>;

                if(passwordValidator != null)
                {
                    var validatedPass = await passwordValidator.ValidateAsync(_userManager,user,model.NewPassword);
                    if (validatedPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher?.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index","Home");
                    }
                }
            }

            return View(model);
        }
    }
}

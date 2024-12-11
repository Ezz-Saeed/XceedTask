using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XceedTask.Models;
using XceedTask.ViewModels;

namespace XceedTask.Controllers
{
    public class AccountsController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager) : Controller
    {

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await userManager.Users.
                FirstOrDefaultAsync(u=>u.Email == registerViewModel.Email || u.UserName == registerViewModel.UserName);
            if(user is not null)
            {
                ModelState.AddModelError("", "Already registered account");
                return View(registerViewModel);
            }

            var appUser = new AppUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };

            var userResult = await userManager.CreateAsync(appUser, registerViewModel.Password);
            if (!userResult.Succeeded)
            {
                var errors = userResult.Errors;
                foreach (var error in errors)
                    ModelState.AddModelError("", error.Description);
                return View(registerViewModel);
            }
            await userManager.AddToRoleAsync(appUser, "user");
            await signInManager.SignInAsync(appUser, isPersistent: false);

            return RedirectToAction("Index","Home");
        }
    }
}

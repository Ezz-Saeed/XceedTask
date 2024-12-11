using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XceedTask.Models;
using XceedTask.Services;
using XceedTask.ViewModels;

namespace XceedTask.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController(IProductService productService, UserManager<AppUser> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            AddProductViewModel viewModel = new()
            {
                Categories = await productService.GetCategories()
            };
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

            var email = User.Claims.SingleOrDefault(u=>u.Type == ClaimTypes.Email)!.Value;
            var appUser = await userManager.FindByEmailAsync(email);
            if(appUser is null)
            {
                ModelState.AddModelError("", "Unauthorized user!");
                return View(viewModel);
            }
            var product = await productService.AddProductAsync(viewModel, appUser);
            if(product is null)
            {
                ModelState.AddModelError("", "Couldn't add product!");
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }


    }
}

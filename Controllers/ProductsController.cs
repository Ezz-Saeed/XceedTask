using AutoMapper;
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
    public class ProductsController(IProductService productService, IMapper mapper,
        UserManager<AppUser> userManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProducts(p=>true);
            return View(products);
        }

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
            return RedirectToAction("GetProducts", "Products");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await productService.GetProductById(id);
            if(product is null) return NotFound();
            var viewModel =  mapper.Map<Product, UpdateProductViewModel>(product);
            viewModel.id = product.Id;
            viewModel.CreationDate = product.CreationDate;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await productService.GetCategories();
                return View(viewModel);
            }
            var product = mapper.Map<UpdateProductViewModel,Product>(viewModel);
            var updatedProduct = await productService.UpdateProduct(product);
            if(updatedProduct is null)
            {
                ModelState.AddModelError("", "Couldn't update product!");
                return View(viewModel);
            }
            return RedirectToAction("GetProducts", "Products");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await productService.GetProductById(id);
            if (product is null) return NotFound();
            var viewModel = mapper.Map<Product, UpdateProductViewModel>(product);
            viewModel.id = product.Id;
            viewModel.CreationDate = product.CreationDate;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel viewModel)
        {
            var result = await productService.Delete(viewModel);

            return RedirectToAction("GetProducts", "Products");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ProductDetails(string id)
        {
            var product = await productService.GetProductById(id);
            if (product is null) return NotFound();
            return View(product);
        }

    }
}

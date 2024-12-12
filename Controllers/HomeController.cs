using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using XceedTask.Models;
using XceedTask.Services;

namespace XceedTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public async Task<IActionResult> Index(string category = null)
        {
            var products = await productService.GetProducts(p=>category.IsNullOrEmpty() ? p.StartDate <= DateTime.Now &&
            p.StartDate.AddDays(p.Duration) >= DateTime.Now :
            p.Category.Name==category && p.StartDate <= DateTime.Now &&
            p.StartDate.AddDays(p.Duration) >= DateTime.Now);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
